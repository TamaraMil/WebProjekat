

export class Racun
{
    constructor(id,broj,stanje,valuta, datumOtvaranja,datumZatvaranja ,vrstaRacuna)
    {
        this.id = id
        this.broj = broj
        this.valuta = valuta
        this.datumOtvaranja = datumOtvaranja
        this.stanje = stanje
        this.vrsta = vrstaRacuna
        this.datumZatvaranja= datumZatvaranja
    }

    draw(place)
    {
        let racunHolder = document.createElement("div")
        racunHolder.className = "racunHolder"
        
        let brojDiv = document.createElement("div")
        brojDiv.className = "stanjeDiv"
        racunHolder.appendChild(brojDiv)

        let lblracuna = document.createElement("label");
        lblracuna.innerHTML = "Broj Racuna:   "
        brojDiv.appendChild(lblracuna);

        let brojRacuna = document.createElement("label")
        brojRacuna.innerHTML = this.broj;
        brojDiv.appendChild(brojRacuna)

        let stanjeDiv = document.createElement("div")
        stanjeDiv.className = "stanjeDiv"
        racunHolder.appendChild(stanjeDiv)

        let lblValuta = document.createElement("label");
        lblValuta.innerHTML = this.valuta + "   ";
        lblValuta.className = "lblValutaRacun"
        stanjeDiv.appendChild(lblValuta);
        
        let stanje = document.createElement("label");
        stanje.innerHTML = this.stanje;
        stanjeDiv.appendChild(stanje);

        // let se = document.getElementsByClassName(".selectorVrsteRacuna")
        // se.value = this.vrsta

        

        let datumDiv = document.createElement("div")
        datumDiv.className = "stanjeDiv"
        racunHolder.appendChild(datumDiv)

        let lblDatumOtvaranja = document.createElement("label");
        lblDatumOtvaranja.innerHTML = "Datum otvaranja: "
        datumDiv.appendChild(lblDatumOtvaranja)

        
        let datumOtvaranja = document.createElement("label");
        datumOtvaranja.innerHTML = this.formatDate(this.datumOtvaranja)
        datumDiv.appendChild(datumOtvaranja)
        let tmp  = this.formatDate(this.datumZatvaranja)
        if(tmp !=="01.01.1")
        {

       
            let datumZatvaranjaDiv = document.createElement("div")
            datumZatvaranjaDiv.className = "stanjeDiv"
            racunHolder.appendChild(datumZatvaranjaDiv)

            let lblDatumZatvaranja = document.createElement("label");
            lblDatumZatvaranja.innerHTML = "Datum Zatvaranja"
            datumZatvaranjaDiv.appendChild(lblDatumZatvaranja)

            let datumZatvaranja = document.createElement("label");
            datumZatvaranja.innerHTML = this.formatDate(this.datumZatvaranja)
            datumZatvaranjaDiv.appendChild(datumZatvaranja)
        }
    
        place.appendChild(racunHolder)




    }

    formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
    
        if (month.length < 2) 
            month = '0' + month;
        if (day.length < 2) 
            day = '0' + day;
    
        return [day, month, year].join('.');
    }
}