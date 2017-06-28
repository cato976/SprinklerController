namespace SprinklerBO
{
    using System;
    public class Zone : IEquatable<Zone>
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int WateringTime { get; set; }
        public StatusType Status { get; set; }

#region IEquatable
        public bool Equals(Zone other)
        {
            return Name == other.Name && Number == other.Number && WateringTime == other.WateringTime;
        }
#endregion IEquatable
    }
}
