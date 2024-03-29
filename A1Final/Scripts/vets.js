﻿/////**
////* This is a simple JavaScript demonstration of how to call MapBox API to load the maps.
////* I have set the default configuration to enable the geocoder and the navigation control.
////* 
////* @author Jian Liew <jian.liew@monash.edu> */
const TOKEN = "pk.eyJ1Ijoic2hheXpoZW5nIiwiYSI6ImNrc3AybmM2cjAxcWcyeHFwdG1vNmZyaGwifQ.d1s2J4nGU8evviOAP0cTYA";
var locations = [];
// The first step is obtain all the latitude and longitude from the HTML // The below is a simple jQuery selector
$(".coordinates").each(function () {
    var FName = $(".FName", this).text().trim();
    var LName = $(".LName", this).text().trim();
    var longitude = $(".longitude", this).text().trim();
    var latitude = $(".latitude", this).text().trim();
    var description = $(".description", this).text().trim(); // Create a point data structure to hold the values.
    var point = {
        "FName": FName, "LName": LName, "latitude": latitude, "longitude": longitude, "description": description
    };
    // Push them all into an array.
    locations.push(point);
});
console.log('locations', locations);
var data = [];
for (i = 0; i < locations.length; i++) {
    var feature = {
        "type": "Feature",
        "properties": {
            "FName": locations[i].FName,
            "LName": locations[i].LName,
            "description": locations[i].description,
            "icon": "circle-15"
        },
        "geometry": {
            "type": "Point",
            "coordinates": [locations[i].longitude, locations[i].latitude]
        }
    };
    data.push(feature)
}
mapboxgl.accessToken = TOKEN;

var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v10',
    zoom: 11,
    center: [locations[0].longitude, locations[0].latitude]
});


map.on('load', function () {
    // Add a layer showing the places. 
    map.addLayer({
        "id": "places",
        "type": "symbol",
        "source": {
            "type": "geojson",
            "data": {
            "type": "FeatureCollection",
                "features": data
        }
    },
    "layout": {
        "icon-image": "{icon}",
            "icon-allow-overlap": true
    }
    });
/*    map.addControl(new MapboxDirections({ accessToken: mapboxgl.accessToken }), 'bottom-left');*/
    const geocoder = new MapboxGeocoder({
        accessToken: mapboxgl.accessToken,
        
       

        marker: {
            color: 'orange'
        },
        mapboxgl: mapboxgl
    });

    map.addControl(geocoder, "top-left");

/*map.addControl(new MapboxGeocoder({ accessToken: mapboxgl.accessToken }));*/

map.addControl(new mapboxgl.NavigationControl());
// Add navigation



// When a click event occurs on a feature in the places layer, open a popup at the 
// location of the feature, with description,Vet's name HTML from its properties.
map.on('click', 'places', function (e) {
    var coordinates = e.features[0].geometry.coordinates.slice();
    var description = e.features[0].properties.description;
    var FName = e.features[0].properties.FName;
    var LName = e.features[0].properties.LName;


// Ensure that if the map is zoomed out such that multiple // copies of the feature are visible, the popup appears
// over the copy being pointed to.
while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
    coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
}

    new mapboxgl.Popup().setLngLat(coordinates).setHTML(FName).setHTML(LName).setHTML(description).addTo(map);
});
// Change the cursor to a pointer when the mouse is over the places layer.
map.on('mouseenter', 'places', function () {
    map.getCanvas().style.cursor = 'pointer';
});
// Change it back to a pointer when it leaves.
map.on('mouseleave', 'places', function () {
    map.getCanvas().style.cursor = '';
});

});



