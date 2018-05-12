using System;
using System.Collections.Generic;

namespace HotDogOrNot.Models
{
    public class VisionResult
    {
        public VisionResult()
        {
            Predictions = new List<Prediction>();
        }

        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}