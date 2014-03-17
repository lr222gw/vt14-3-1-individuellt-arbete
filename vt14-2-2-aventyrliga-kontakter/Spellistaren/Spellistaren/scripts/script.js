var h = document.getElementById("PHContent_GameDetailRepeater_ReleaseDate");
if (h.value == "1/1/0001") {
    h.value = ""; //om realeasedate datumet är tomt så är den inställd att vara datumet "1/1/0001" 
                        //detta pga krångel med null. Här ändrar vi värdet efter att det har satts..
}