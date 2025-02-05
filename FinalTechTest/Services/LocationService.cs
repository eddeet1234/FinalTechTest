namespace FinalTechTest.Services
{
    using System;

    public class LocationService
    {
        public event Action? OnLocationChanged;
        public event Action? OnAddMarkerRequested; // New event for triggering marker addition
        public event Action? UpdateMarkerFromInputAction; // New event for triggering marker addition
        public event Action? UpdateMarkerFromMapAction; // New event for triggering marker moving
        private double? latitude;
        private double? longitude;
        private string? name;
        private double? _newLatitude = 0;
        private double? _newLongitude = 0;


        public void SetLocation(double lat, double lng, string locName)
        {
            latitude = lat;
            longitude = lng;
            name = locName;
            OnLocationChanged?.Invoke(); // Notify listeners that location has changed
        }

        public double? GetLatitude()
        {
            return latitude;
        }

        public double? GetLongitude()
        {
            return longitude;
        }

        public string? GetName()
        {
            return name;
        }


        public void RequestAddMarker()
        {
            OnAddMarkerRequested?.Invoke(); // Notify listeners that a new marker should be added
        }

        public void UpdateMarkerfromInput(double newLatitude, double newLongitude)
        {
            _newLatitude = newLatitude;
            _newLongitude = newLongitude;
            UpdateMarkerFromInputAction?.Invoke();
        }

        public void UpdateMarkerfromMap(double newLatitude, double newLongitude)
        {
            _newLatitude = newLatitude;
            _newLongitude = newLongitude;
            UpdateMarkerFromMapAction?.Invoke();
        }

        public double? GetNewLatitude()
        {
            return _newLatitude;
        }
        public double? GetNewLongitude()
        {
            return _newLongitude;
        }
    }
}
