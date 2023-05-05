const API_URL = "http://api.metals.live/v1/spot";
const METALS = ['gold', 'silver', 'platinum', 'palladium'];

async function fetchMetals() {
    try {
        const response = await fetch(API_URL);
        if(!response.ok) {
            throw new Error("Something went wrong, while fatching data!")
        }
        const data = await response.json();
        // Set current timestamp
        document.getElementById("date").textContent = new Date().toISOString().slice(0, 19).replace('T', ' ');
        METALS.forEach(name => {
            const metal = data.find(metal => metal[name]);
            if (metal) {
                const value = metal[name];
                document.getElementById(name).innerHTML = `${value} <font>per toz</font>`;
            }
        });
    }
    catch (error) {
        console.error(error);
    }
}

fetchMetals();

// Fetch data every 10 seconds
setInterval(fetchMetals, 10000);