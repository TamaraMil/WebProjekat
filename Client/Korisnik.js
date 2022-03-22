export class Korisnik
{
    constructor(id,ime,prezime, jmbg, datumRodjenja,adresa, racuni)
    {
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.jmbg = jmbg;
        this.datumRodjenja = datumRodjenja;
        this.adresa = adresa;
        this.racuni = racuni;
    }
    drawInfoUser(place)
    {
        let lblIme = document.createElement("label");
        let lblPrezime = document.createElement("label");
        let lblJMBG = document.createElement("label");
        let lblDatumRodjenja = document.createElement("label");
        let lblAdresa = document.createElement("label");

        lblIme.innerHTML ="Ime klienta:  " + this.ime;
        lblPrezime.innerHTML ="Prezime klienta:  " + this.prezime;
        lblJMBG.innerHTML = "JMBG:  " + this.jmbg;
        lblDatumRodjenja.innerHTML ="Datum Rodjenja:  " + this.formatDate(this.datumRodjenja);
        lblAdresa.innerHTML ="Adresa:  " + this.adresa;

        lblIme.className = "lblInfo";
        lblPrezime.className = "lblInfo";
        lblJMBG.className = "lblInfo";
        lblDatumRodjenja.className = "lblInfo";
        lblAdresa.className = "lblInfo";

        place.appendChild(lblIme);
        place.appendChild(lblPrezime);
        place.appendChild(lblJMBG);
        place.appendChild(lblDatumRodjenja);
        place.appendChild(lblAdresa);

    }
    setRacuni(racun)
    {
        this.racuni = racun
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