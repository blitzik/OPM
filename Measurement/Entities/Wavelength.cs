namespace Measurement.Entities
{
    public class Wavelength
    {
        public string Key { get; }
        public int Value { get; }
        
        
        public Wavelength(string key, int wavelength)
        {
            Key = key;
            Value = wavelength;
        }
    }
}