var map = null
window.initMapWithMarkers = (elementId, locations, dotNetHelper) => {
    map = L.map(elementId).setView([0, 0], 2); // Default view, will auto-adjust

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '' // Removes the attribution text
    }).addTo(map);

    var bounds = [];

    locations.forEach(location => {
        var marker = L.marker([location.latitude, location.longitude]).addTo(map)
            .bindPopup(`<b>${location.name}</b><br>Lat: ${location.latitude}<br>Lng: ${location.longitude}`);

        bounds.push([location.latitude, location.longitude]);

        // Listen for marker click
        marker.on('click', function () {
            dotNetHelper.invokeMethodAsync('UpdateSelectedMarker', location.latitude, location.longitude, location.name);
        });
    });

    if (bounds.length > 0) {
        map.fitBounds(bounds); // Adjust map to fit all markers
    }

    
};

window.addDraggableMarker = (elementId, dotNetHelper) => {

    var center = map.getCenter(); // Get current center of the map
    var lat = center.lat;
    var lng = center.lng;

    window.currentMarker = L.marker([lat, lng], { draggable: true }).addTo(map)
        .bindPopup("Drag me to adjust position").openPopup();

    // Notify Blazor when the marker is moved
    window.currentMarker.on('dragend', function () {
        var position = window.currentMarker.getLatLng();
        dotNetHelper.invokeMethodAsync('UpdateNewMarkerPosition', position.lat, position.lng);
    });
};

window.updateMarkerPosition = (elementId, lat, lng) => {
    if (window.currentMarker) {
        window.currentMarker.setLatLng([lat, lng]);
    }
};