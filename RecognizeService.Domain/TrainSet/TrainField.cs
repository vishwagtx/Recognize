using RecognizeService.Domain.TrainSet.Enum;

namespace RecognizeService.Domain.TrainSet
{
    public class TrainField
    {
        public string Name { get; set; }
        public string MapingField { get; set; }
        public string MapingFunction { get; set; }
        public SearchType SearchType { get; set; }
        public DirectionType Direction { get; set; }
        public FieldType Type { get; set; }
        public string PaserFormat { get; set; }
        public double Y_PossitionDiff { get; set; }
        public double X_PossitionDiff { get; set; }
        public int? NoOfLines { get; set; }
    }
}
