var map;
var startMarker;
var destinationMarker;
var directionsService;
var directionsRenderer;

console.log("Google Map JS Library Loaded!");

let startedOn;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 0, lng: 0 },
        zoom: 12
    });

    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer({
        map: map
    });

    var startAutocomplete = new google.maps.places.Autocomplete(document.getElementById('startLocation'));
    var destinationAutocomplete = new google.maps.places.Autocomplete(document.getElementById('destinationLocation'));

    startAutocomplete.addListener('place_changed', function () {
        var place = startAutocomplete.getPlace();
        if (place.geometry) {
            map.panTo(place.geometry.location);
            map.setZoom(15);
            placeStartMarker(place.geometry.location);
        }
    });

    destinationAutocomplete.addListener('place_changed', function () {
        var place = destinationAutocomplete.getPlace();
        if (place.geometry) {
            map.panTo(place.geometry.location);
            map.setZoom(15);
            placeDestinationMarker(place.geometry.location);
        }
    });
}

function placeStartMarker(location) {
    if (startMarker) {
        startMarker.setMap(null);
    }
    startMarker = new google.maps.Marker({
        position: location,
        map: map
    });
}

function placeDestinationMarker(location) {
    if (destinationMarker) {
        destinationMarker.setMap(null);
    }
    destinationMarker = new google.maps.Marker({
        position: location,
        map: map
    });

    calculateAndDisplayRoute();
}

function calculateAndDisplayRoute() {
    var start = startMarker.getPosition();
    var destination = destinationMarker.getPosition();

    directionsService.route(
        {
            origin: start,
            destination: destination,
            travelMode: google.maps.TravelMode.DRIVING
        },
        function (response, status) {
            if (status === google.maps.DirectionsStatus.OK
            ) {
                directionsRenderer.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
}

function playRoute() {
    if (!startMarker || !destinationMarker) {
        window.alert('Please set both start and destination markers.');
        return;
    }

    // disable fields
    document.getElementById("startLocation").disabled = true;
    document.getElementById("destinationLocation").disabled = true;

    // start date
    startedOn = new Date().toISOString().slice(0, 19).replace('T', ' ');
    document.getElementById("startTime").innerHTML = "<b>Started on:</b> " + startedOn;

    var start = startMarker.getPosition();
    var destination = destinationMarker.getPosition();

    var distance = google.maps.geometry.spherical.computeDistanceBetween(start, destination);
    var durationInSeconds = distance / 50; // Assuming an average speed of 50 meters per second

    var step = 0;
    var interval = setInterval(function () {

        // Destination Reached!
        if (step >= durationInSeconds) {
            clearInterval(interval);
            window.alert('Destination reached!');
            createTrip();
            return;
        }

        var fraction = step / durationInSeconds;
        var newPosition = google.maps.geometry.spherical.interpolate(start, destination, fraction);
        startMarker.setPosition(newPosition);
        map.panTo(newPosition);

        step++;
    }, 1000); // Update position every second
}

function createTrip() {
    // Create a new trip object with the required properties
    const trip = {
        startLocation: document.getElementById("startLocation").value,
        destinationLocation: document.getElementById("destinationLocation").value,
        startedOn: startedOn
    };

    fetch('/trips', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(trip)
    })
        .then(response => {
            if (response.ok) {
                // Trip created successfully
                console.log('New trip created!');
                window.location.href = "/trips";
            } else {
                // Handle error response
                console.error('Failed to create trip:', response.statusText);
            }
        })
        .catch(error => {
            // Handle network or fetch API errors
            console.error('Error creating trip:', error);
        });
}
