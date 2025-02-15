var map = null
var bounds = [];
let markers = {};


window.initMapWithMarkers = (elementId, locations, dotNetHelper) => {
    map = L.map(elementId).setView([0, 0], 2); // Default view, will auto-adjust

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '' // Removes the attribution text
    }).addTo(map);

    

    locations.forEach(location => {
        const marker = L.marker([location.latitude, location.longitude]).addTo(map)
            .bindPopup(`<b>${location.name}</b><br>Lat: ${location.latitude}<br>Lng: ${location.longitude}`);

        bounds.push([location.latitude, location.longitude]);

        markers[location.name] = marker; // Store marker reference
        
        // Listen for marker click
        marker.on('click', function () {
            dotNetHelper.invokeMethodAsync('UpdateSelectedMarker', location.latitude, location.longitude, location.name);
            centerMapOnMarker(location.latitude, location.longitude);
        });
    });

    if (bounds.length > 0) {
        map.fitBounds(bounds, {
            padding: [50, 50] // Add padding to zoom out a bit more
        });
    
    }

    // Add the home button
    addHomeButton(map);
};

window.addDraggableMarker = (elementId, dotNetHelper) => {

    var center = map.getCenter(); // Get current center of the map
    var lat = center.lat;
    var lng = center.lng;

    window.currentMarker = L.marker([lat, lng], { draggable: true }).addTo(map)
        .bindPopup("Drag me to adjust the position").openPopup();

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

window.lockMarkerAndAddPopup = (elementId, name, lat, lng, dotNetHelper) => {
    if (window.currentMarker) {
        // Disable dragging
        window.currentMarker.dragging.disable();

        // Add popup with the specified content
        window.currentMarker.bindPopup(`<b>${name}</b><br>Lat: ${lat}<br>Lng: ${lng}`).openPopup();

        // Store marker reference
        markers[name] = window.currentMarker; 

        // Set the current location to the added location
        dotNetHelper.invokeMethodAsync('UpdateSelectedMarker', lat, lng, name);

        // Bind click event to the marker
        window.currentMarker.on('click', function () {
            dotNetHelper.invokeMethodAsync('UpdateSelectedMarker', lat, lng, name);
            centerMapOnMarker(lat,lng)
        });

        bounds.push([lat, lng]);
    }

};

window.removeMarker = () => {
    if (window.currentMarker) {
        // Remove the marker from the map
        window.currentMarker.remove();
        window.currentMarker = null; // Clear the reference
    }
};


// Function to add a custom "Home" button
function addHomeButton(map) {
    var homeControl = L.Control.extend({
        options: {
            position: 'topleft' // Position it near the zoom controls
        },

        onAdd: function (map) {
            var container = L.DomUtil.create('div', 'leaflet-bar leaflet-control leaflet-control-custom home-button');
            container.innerHTML = '<i class="fas fa-home" style="color: black;"></i>'; // Font Awesome house icon

            container.onclick = function () {
                if (bounds.length > 0) {
                    map.fitBounds(bounds, {
                        padding: [50, 50] // Add padding to zoom out a bit more
                    });
                }
            };

            return container;
        }
    });

    map.addControl(new homeControl());
}

function openMarkerPopup(locationName) {
    const marker = markers[locationName];
    if (marker) {
        marker.openPopup();
        centerMapOnMarker(marker.getLatLng().lat, marker.getLatLng().lng);
    }
}

function centerMapOnMarker(lat, lng) {
    map.setView([lat, lng],5);
}