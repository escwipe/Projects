# Metals Price Overview
This is a simple web application that fetches the current market prices of four precious metals, i.e., Gold, Silver, Platinum, and Palladium, from the Metals Live API (http://api.metals.live/v1/spot).

## Stack
* JavaScript
* HTML
* CSS

## How it Works
The application uses the fetch method to get the data from the Metals Live API, parses it using JSON, and then displays the current market prices of the metals in the HTML document. The data is updated every 10 seconds using setInterval.

The application displays the metal names, their corresponding prices, and a small legend stating that 1 toz (troy ounce) equals 31.103 grams. Additionally, the application shows the last update time at the bottom of the page.