namespace RecognizeService.Domain.TrainSet
{
    public class TrainModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public IList<TrainField> Fields { get; set; }
    }
}
