namespace FinalTechTest.Services
{
    using System;

    public class LocationService
    {
        public event Action? OnLocationChangedfromMapAction; //Location 
        public event Action? OnLocationChangedFromInputAction;
        public event Action? OnAddMarkerRequestedAction; // New event for triggering marker addition
        public event Action? UpdateMarkerFromInputAction; // New event for triggering marker addition
        public event Action? UpdateMarkerFromMapAction; // New event for triggering marker moving

        public Func<Task>? LockMarkerAndAddPopupAction;
        public event Action? RemoveMarkerAction; // New event for triggering marker removal
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Name { get; private set; }


        public string MarkerName { get; set; }
        public double MarkerLatitude { get; private set; }
        public double MarkerLongitude { get; private set; }




        public void SetLocation(double lat, double lng, string name, string source)
        {
            Latitude = lat;
            Longitude = lng;
            Name = name;
            if (source == "map")
                OnLocationChangedFromInputAction?.Invoke(); // Notify listeners on Blazor side that location has changed
            if (source == "input")
                OnLocationChangedFromInputAction?.Invoke(); // Notify listeners on Map side that location has changed
        }

        public void RequestAddMarker()
        {
            OnAddMarkerRequestedAction?.Invoke(); // Notify listeners that a new marker should be added
        }

        public void UpdateMarkerfromInput(double newLatitude, double newLongitude)
        {
            MarkerLatitude = newLatitude;
            MarkerLongitude = newLongitude;
            UpdateMarkerFromInputAction?.Invoke();
        }

        public void UpdateMarkerfromMap(double newLatitude, double newLongitude)
        {
            MarkerLatitude = newLatitude;
            MarkerLongitude = newLongitude;
            UpdateMarkerFromMapAction?.Invoke();
        }

        public void SetMarkerName(string name)
        {
            MarkerName = name;
        }

        public async Task LockMarkerAndAddPopup()
        {
            if (LockMarkerAndAddPopupAction != null)
            {
                await LockMarkerAndAddPopupAction.Invoke(); //Ensures execution is awaited
            }
        }

        public void RemoveMarker()
        {
            RemoveMarkerAction?.Invoke();
        }
    }
}
