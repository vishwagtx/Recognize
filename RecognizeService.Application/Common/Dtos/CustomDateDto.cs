using System.Globalization;

namespace RecognizeService.Application.Common.Dtos
{
    public class CustomDateDto
    {
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string? OriginalString { get; set; }
        public bool SuccessfullyParsed { get; set; } = true;

        public static CustomDateDto ConvertToCustomDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return new CustomDateDto
                {
                    Day = dateTime.Value.Day,
                    Month = dateTime.Value.Month,
                    Year = dateTime.Value.Year,
                    OriginalString = dateTime.Value.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                };

            return new CustomDateDto
            {
                SuccessfullyParsed = false,
            };
        }
    }
}
