using Azure.AI.FormRecognizer.Models;
using RecognizeService.Domain.DataExtract;
using RecognizeService.Domain.TrainSet;
using RecognizeService.Domain.TrainSet.Enum;

namespace RecognizeService.Application.ML
{
    internal static class DataExtractML
    {
        public static (bool ValidDocument, IDictionary<string, object> Values) Extract(TrainModelResult trainDataSet, IReadOnlyList<FormLine> lines)
        {
            if (trainDataSet == null || lines == null) return (false, null);

            bool validDocument = lines.Any(l => trainDataSet.Models.Select(m => m?.Text?.ToLower()).Contains(l?.Text?.ToLower()));

            if (!validDocument) return (false, null);

            var avialbleModelText = lines.FirstOrDefault(l => trainDataSet.Models.Select(m => m?.Text?.ToLower()).Contains(l?.Text?.ToLower()));
            var fields = trainDataSet.Models.FirstOrDefault(m => m.Text.ToLower() == avialbleModelText.Text.ToLower())?.Fields ?? new List<TrainField>();

            IReadOnlyList<FormLine> machings;
            Dictionary<string, object> values = new Dictionary<string, object>();

            foreach (var filed in fields)
            {
                if (filed.SearchType == SearchType.BoundrySearch)
                {
                    BoundrySearch(lines, values, filed);
                }
                else if (filed.SearchType == SearchType.TextSearch)
                {
                    TextSearch(lines, trainDataSet, values, filed);
                }
            }

            return (true, values);
        }

        private static void BoundrySearch(IReadOnlyList<FormLine> lines, Dictionary<string, object> values, TrainField filed)
        {
            FormLine? line = lines?.FirstOrDefault(f => f.Text.ToLower().Contains(filed?.MapingField.ToLower()));
            if (line == null) return;

            Position topLeft = new()
            {
                X = line?.BoundingBox[0].X ?? 0,
                Y = line?.BoundingBox[0].Y ?? 0,
            };

            IReadOnlyList<FormLine> machings;
            if (filed.Direction == DirectionType.Column)
            {
                machings = lines.Where(l => (l.BoundingBox[0].Y >= topLeft.Y && l.BoundingBox[0].Y <= topLeft.Y + filed.Y_PossitionDiff)
                && (l.BoundingBox[0].X >= topLeft.X && l.BoundingBox[0].X <= topLeft.X + filed.X_PossitionDiff)).ToList();
            }
            else
            {
                machings = lines.Where(l => l.BoundingBox[0].Y >= topLeft.Y && l.BoundingBox[0].Y <= topLeft.Y + filed.Y_PossitionDiff).ToList();
            }

            if (machings.Count > 1)
                values.Add(filed.Name, GetValue(filed, machings));
        }

        private static void TextSearch(IReadOnlyList<FormLine> lines, TrainModelResult trainDataSet, Dictionary<string, object> values, TrainField filed)
        {
            switch (filed.MapingFunction)
            {
                case "GetNationality":
                    var natinality = CountrySearchML.GetNationality(lines.Select(s => s.Text).ToArray(), trainDataSet.Countries);
                    values.Add(filed.Name, natinality);
                    break;
            }
        }

        private static object GetValue(TrainField filed, IReadOnlyList<FormLine> lines)
        {
            switch (filed.Type)
            {
                case FieldType.String:
                    return filed.NoOfLines == null ?
                        lines[1].Text : string.Join(", ", lines.Where(a => a.Text != lines.FirstOrDefault().Text).Select(a => a.Text));

                case FieldType.Date:
                    return DateTime.ParseExact(lines[1].Text, filed.PaserFormat, null);

                case FieldType.StringArray:
                    return lines.Where(a => a.Text != lines.FirstOrDefault().Text).Select(a => a.Text).ToArray();
            }

            return "";
        }
    }
}
