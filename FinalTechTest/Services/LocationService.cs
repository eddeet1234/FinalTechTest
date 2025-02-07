namespace FinalTechTest.Services
{
    using System;

    public class LocationService
    {
        public event Action? OnLocationChanged;
        public event Action? OnLocationChangedFromInput;
        public event Action? OnAddMarkerRequested; // New event for triggering marker addition
        public event Action? UpdateMarkerFromInputAction; // New event for triggering marker addition
        public event Action? UpdateMarkerFromMapAction; // New event for triggering marker moving
    
        public Func<Task>? LockMarkerAndAddPopupAction;
        public event Action? RemoveMarkerAction; // New event for triggering marker removal
        private double latitude;
        private double longitude;
        private string name;
        private string? _newName;
        private double? _newLatitude = 0;
        private double? _newLongitude = 0;
        



        public void SetLocation(double Lat, double Lng, string Name, string locName)
        {
            latitude = Lat;
            longitude = Lng;
            name = Name;
            if(locName == "map")
                OnLocationChanged?.Invoke(); // Notify listeners on Blazor side that location has changed
            if(locName == "input")
                OnLocationChangedFromInput?.Invoke(); // Notify listeners on Map side that location has changed
        }

        public double GetLatitude()
        {
            return latitude;
        }

        public double GetLongitude()
        {
            return longitude;
        }

        public string GetName()
        {
            return name;
        }


        public void RequestAddMarker()
        {
            OnAddMarkerRequested?.Invoke(); // Notify listeners that a new marker should be added
        }

        #region Get and Set for New location
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

        
        public void SetNewName(string newName)
        {
            _newName = newName;
        }

        public string? GetNewName()
        {
            return _newName;
        }

        public double? GetNewLatitude()
        {
            return _newLatitude;
        }


        public double? GetNewLongitude()
        {
            return _newLongitude;
        }
        #endregion

        public async Task LockMarkerAndAddPopup()
        {
            if (LockMarkerAndAddPopupAction != null)
            {
                await LockMarkerAndAddPopupAction.Invoke(); // ✅ Ensures execution is awaited
            }
        }

        public void RemoveMarker()
        {
            RemoveMarkerAction?.Invoke();
        }
    }
}
