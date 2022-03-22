import { Korisnik } from "./Korisnik.js";
import { VrsteRacuna } from "./VrsteRacuna.js";
import { Racun } from "./Racun.js";


export class Banka
{
    constructor(ime)
    {
        this.ime = ime;
        this.container1 = null;
        this.container2 =
        this.loginContainer = null;
        this.klientContainer = null;
        this.racuniContainer = null;
        this.Admin = false;
        this.korisnikid = null
        this.korisnik = null;
        this.vrsteRacuna = []

    }

    drawPage(host)
    {
    

        this.container = document.createElement("div");
        this.container.className ="container";
        host.appendChild(this.container);

        let title = document.createElement("h1");
        title.className = "title";
        title.innerHTML = "TM BANKA";
        this.container.appendChild(title);

        this.loginContainer = document.createElement("div");
        this.loginContainer.className ="loginContainer";
        this.container.appendChild(this.loginContainer);
        console.log( this.loginContainer)

        this.drawLoginContainer( this.loginContainer);
        console.log(this.container);

    }
    // Drawing Login page 
    drawLoginContainer(gde)
    {


        let emailContainer = document.createElement("div");
        let passwordContainer = document.createElement("div");
        let btnContainer = document.createElement("div");

        emailContainer.className = "loginRow";
        passwordContainer.className = "loginRow";

        let labelEmail = document.createElement("label");
        labelEmail.innerHTML = "Email";
        labelEmail.className = "loginText";
        emailContainer.appendChild(labelEmail);

        let inputEmail = document.createElement("input");
        inputEmail.type = "text";
        inputEmail.className = "loginInput"
        inputEmail.placeholder = "Email";
        emailContainer.appendChild(inputEmail);

        let labelPassword = document.createElement("label");
        labelPassword.innerHTML = "Password";
        labelPassword.className = "loginText";
        passwordContainer.appendChild(labelPassword);

        let inputPassword = document.createElement("input");
        inputPassword.type = "password";
        inputPassword.className = "loginInput"
        inputPassword.placeholder = "Password";
        passwordContainer.appendChild(inputPassword);

        let btn = document.createElement("button");
        btn.innerHTML = "Login";
        btn.className = "btnLogin"
        btnContainer.className = "loginRow"
        
        btn.onclick = (ev)=>this.login(inputEmail.value,inputPassword.value);

        btnContainer.appendChild(btn);
        
        gde.appendChild(emailContainer);
        gde.appendChild(passwordContainer);
        gde.appendChild(btnContainer);

    

    }
    // Function for loging in
    login(email,password)
    {
        
        if(email === null || email ===undefined || email === "")
        {
            alert("Unesite email");
            return;
        }
        if(password === null || password ===undefined || password === "")
        {
            alert("Unesite password");
            return;
        }
        
        var emailPassword =btoa(email + ":" + password);
        fetch("https://localhost:5001/Nalog/Login/" + emailPassword,
        {
            method: "GET"
        }).then(n=>
            {
                   if(n.ok){
                    n.json().then(p=>
                        {
                            this.Admin = p.admin;
                            this.korisnikid = p.korisnikID;
                            console.log(p.admin,p.korisnikID);
                        });
                   }
                   else
                   {
                       alert("Pogresan mejl ili lozinka");
                       return;
                   }
                   this.deleteOldContent();
            })
            .catch(p=>
                {
                    alert ("Greška u pozivu prikazu ");
                });

        console.log(this.Admin,this.korisnikid);
        
    }
    // Deleting login content and calling function for creating user or admin content 
    deleteOldContent()
    {
        var tmp = document.querySelector(".loginContainer");
        var parent = tmp.parentNode;
        parent.removeChild(tmp);

    
       
        setTimeout(() => { this.drawUser(this.container)},500);
    

    }

    drawUser(place)
    {
        let korisnikContainer = document.createElement("div");
        korisnikContainer.className = "clientContainer";
        place.appendChild(korisnikContainer);
        this.korisnikContainer = korisnikContainer
        let clientInfo = document.createElement("div");
        clientInfo.className = "clientInfoContainer" ;

        if(this.Admin)
        {
           let adminCliendContainer = document.createElement("div");
           adminCliendContainer.className = "adminClient"
           this.korisnikContainer.appendChild(adminCliendContainer);
           this.korisnikContainer.appendChild(clientInfo);
           console.log("uso")
           let lblAdmin = document.createElement("label");
           lblAdmin.className = "textAdminLbl"
           lblAdmin.innerHTML = "JMBG: "
           adminCliendContainer.appendChild(lblAdmin);
           let inputJMBG = document.createElement("input");
           inputJMBG.type = "number";
           inputJMBG.placeholder = "JMBG"
           adminCliendContainer.appendChild(inputJMBG);

           let btnPretrazi = document.createElement("button");
           btnPretrazi.innerHTML = "Pretrazi korisnika";
           btnPretrazi.className = "clientInfoPretrazi"
           btnPretrazi.onclick = (ev) => this.pretraziKorisnike(inputJMBG.value,clientInfo);
           adminCliendContainer.appendChild(btnPretrazi);  
           
           let btnDodaj = document.createElement("button");
           btnDodaj.innerHTML = "Dodaj klijenta";
           btnDodaj.className = "btnDodajklijenta";
           adminCliendContainer.appendChild(btnDodaj);
           btnDodaj.onclick = (ev)=>this.addKorisnika(clientInfo, adminCliendContainer);

        }
        
        if(this.Admin == false)
        {
            this.korisnikContainer.appendChild(clientInfo);
            this.pretraziKorisnikeID(this.korisnikid,clientInfo);
        }
        

        let racuniContainer =this.addRow(this.container);
        racuniContainer.className = "racuniContainer";
        let selectorRacunaContainer = this.addRow(racuniContainer);
        let racuniInfoContainer = this.addRow(racuniContainer);
        racuniInfoContainer.className = "racuniInfoContainer"

        selectorRacunaContainer.className = "selectorContainer"
        let lblTip = document.createElement("label")
        lblTip.innerHTML = "Tip Racuna:"
        selectorRacunaContainer.appendChild(lblTip)

        this.addVrsteRacuna(selectorRacunaContainer);
        
        if(this.Admin == true)
        {
            let btnUnesiRacun = document.createElement("button");
      

            btnUnesiRacun.onclick = (ev)=>this.dodajFormu(selectorRacunaContainer,racuniInfoContainer,btnUnesiRacun)
            btnUnesiRacun.innerHTML = "Unesi Racun"
            selectorRacunaContainer.appendChild(btnUnesiRacun);

            let btnIzmeniRacun = document.createElement("button");
            btnIzmeniRacun.innerHTML = "Izmeni Racun"
            selectorRacunaContainer.appendChild(btnIzmeniRacun);

            btnIzmeniRacun.onclick = (ev)=>this.dodajFormuIzmeni(selectorRacunaContainer)

            let btnZatvoriRacun = document.createElement("button");
            btnZatvoriRacun.innerHTML = "Zatvori racun "
            selectorRacunaContainer.appendChild(btnZatvoriRacun);

            btnZatvoriRacun.onclick = (ev)=>this.dodajFormuZatvori(selectorRacunaContainer)
            

            let btnIzbrisi = document.createElement("button");
            btnIzbrisi.innerHTML = "Izbrisi racun "
            selectorRacunaContainer.appendChild(btnIzbrisi);

            btnIzbrisi.onclick = (ev)=>this.dodajIzbrisi(selectorRacunaContainer)

            
            
        }

    }
    dodajFormuZatvori(gde)
    {
        let forma = document.createElement("div");
        forma.className = "formaZatvori"
        let lblBrojRacuna = document.createElement("label");
        lblBrojRacuna.innerHTML = "Broj Racuna:";
        forma.appendChild(lblBrojRacuna);

        let inputBrojRacuna = document.createElement("input");
        inputBrojRacuna.type = "number";
        inputBrojRacuna.placeholder = "Uneti broj racuna";
        forma.appendChild(inputBrojRacuna)

        let lblZatvaranja = document.createElement("label");
        lblZatvaranja.innerHTML = "Datum Zatvaranja:"
        forma.appendChild(lblZatvaranja)

        let inputDatumZatvaranja = document.createElement("input");
        inputDatumZatvaranja.type = "date"
        inputDatumZatvaranja.placeholder = "datum";
        forma.appendChild(inputDatumZatvaranja);

        let btnDodajRacun = document.createElement("button");
        btnDodajRacun.innerHTML = "Zatvori"
        btnDodajRacun.onclick = (ev)=>this. ZatvoriRacun(inputBrojRacuna.value,inputDatumZatvaranja.value.toString())
        forma.appendChild(btnDodajRacun);
        gde.appendChild(forma)
    }

    dodajFormuIzmeni(gde)
    {
        let forma = document.createElement("div");
        forma.className = "formaIzmeni"
        let lblBrojRacuna = document.createElement("label");
        lblBrojRacuna.innerHTML = "Broj Racuna:";
        forma.appendChild(lblBrojRacuna);

        let inputBrojRacuna = document.createElement("input");
        inputBrojRacuna.type = "number";
        inputBrojRacuna.placeholder = "Uneti broj racuna";
        forma.appendChild(inputBrojRacuna)

        let lblStanje = document.createElement("label");
        lblStanje.innerHTML = "Stanje:";
        forma.appendChild(lblStanje);

        let inputStanjeRacuna = document.createElement("input");
        inputStanjeRacuna.type = "number";
        inputStanjeRacuna.placeholder = "Uneti novo stanje";
        forma.appendChild(inputStanjeRacuna)

        let lblvaluta = document.createElement("label");
        lblvaluta.innerHTML = "Valuta:"
        forma.appendChild(lblvaluta)

        let inputValuta = document.createElement("input");
        inputValuta.type = "text";
        inputValuta.placeholder = "Uneti valutu  racuna";
        forma.appendChild(inputValuta);

        let lblOtvaranja = document.createElement("label");
        lblOtvaranja.innerHTML = "Datum Otvaranja:"
        forma.appendChild(lblOtvaranja)

        let inputDatumOtvaranja = document.createElement("input");
        inputDatumOtvaranja.type = "date"
        inputDatumOtvaranja.placeholder = "datum";
        forma.appendChild(inputDatumOtvaranja);

        let btnDodajRacun = document.createElement("button");
        btnDodajRacun.innerHTML = "Izmeni racun"
        btnDodajRacun.onclick = (ev)=>this.promeniRacun(inputBrojRacuna.value,inputStanjeRacuna.value,inputValuta.value,inputDatumOtvaranja.value.toString())
        forma.appendChild(btnDodajRacun);
        gde.appendChild(forma)
    }
    addkorisnik()
    {
        let korisnikContainer = document.createElement("div");
        korisnikContainer.className = "clientContainer";
        place.appendChild(korisnikContainer);
        this.korisnikContainer = korisnikContainer
        let clientInfo = document.createElement("div");
        clientInfo.className = "clientInfoContainer" ;

        if(this.Admin)
        {
           let adminCliendContainer = document.createElement("div");
           adminCliendContainer.className = "adminClient"
           this.korisnikContainer.appendChild(adminCliendContainer);
           this.korisnikContainer.appendChild(clientInfo);
           console.log("uso")
           let lblAdmin = document.createElement("label");
           lblAdmin.className = "textAdminLbl"
           lblAdmin.innerHTML = "JMBG: "
           adminCliendContainer.appendChild(lblAdmin);
           let inputJMBG = document.createElement("input");
           inputJMBG.type = "number";
           inputJMBG.placeholder = "JMBG"
           adminCliendContainer.appendChild(inputJMBG);

           let btnPretrazi = document.createElement("button");
           btnPretrazi.innerHTML = "Pretrazi korisnika";
           btnPretrazi.className = "clientInfoPretrazi"
           btnPretrazi.onclick = (ev) => this.pretraziKorisnike(inputJMBG.value,clientInfo);
           adminCliendContainer.appendChild(btnPretrazi);  
           
           let btnDodaj = document.createElement("button");
           btnDodaj.innerHTML = "Dodaj klijenta";
           btnDodaj.className = "btnDodajklijenta";
           adminCliendContainer.appendChild(btnDodaj);
           btnDodaj.onclick = (ev)=>this.addKorisnika(clientInfo, adminCliendContainer);

        }
        
        if(this.Admin == false)
        {
            this.korisnikContainer.appendChild(clientInfo);
            this.pretraziKorisnikeID(this.korisnikid,clientInfo);
        }
    }

    pretraziKorisnike(jmbg,clientPom)
    {   

        if(jmbg.length != 13)
        {
            alert("Jmbg treba da ima 13 cifara!");
        }
        fetch("https://localhost:5001/Korisnik/Korsinici/" + jmbg,
        {
            method: "GET"
        })
        .then(k=> {
            if(k.ok)
            {
                k.json().then(p=>{
                    console.log(p)
                    let korisnik = new Korisnik(p.id,p.ime,p.prezime,p.jmbg,p.datumRodjenja, p.adresa);
                    this.korisnikID = p.id
                    korisnik.drawInfoUser(clientPom);
                    this.isapisatiRacune()

                })
            }
        });

        //this.preuzmiRacune(kori)


    }
    pretraziKorisnikeID(id,container)
    {   

        if(jmbg.length != 13)
        {
            alert("Jmbg treba da ima 13 cifara!");
            return;
        }
        fetch("https://localhost:5001/Korisnik/KorsiniciID/" + id,
        {
            method: "GET"
        })
        .then(k=> {
            if(k.ok)
            {
                k.json().then(p=>{
                    console.log(p)
                    this.korisnik = new Korisnik(p.id,p.ime,p.prezime,p.jmbg,p.datumRodjenja, p.adresa);
                    korisnik.drawInfoUser(container);
                    
                })
            }
        });
    }
    addKorisnika(place, placeForBtn)
    {
        this.deleteElementswith("clientInfoContainer")
        let newContainer = this.addRow(this.korisnikContainer)
        newContainer.className = "clientInfoContainer"

        let pom = document.querySelector(".btnDodajklijenta");
        let tmp = pom.parentNode;
        tmp.removeChild(pom);



        let lblIme = document.createElement("label");
        let lblPrezime = document.createElement("label");
        let lblJMBG = document.createElement("label");
        let lblDatumRodjenja = document.createElement("label");
        let lblAdresa = document.createElement("label");

        lblIme.innerHTML ="Ime klienta:  ";
        lblPrezime.innerHTML ="Prezime klienta:  ";
        lblJMBG.innerHTML = "JMBG:  ";
        lblDatumRodjenja.innerHTML ="Datum Rodjenja:  ";
        lblAdresa.innerHTML ="Adresa:  ";

        let inputIme = document.createElement("input");
        inputIme.type = "text"
        inputIme.placeholder = "Ime";
        let inputPrezime = document.createElement("input");
        inputPrezime.type = "text"
        inputPrezime.placeholder = "Prezime";
        let inputJMBG = document.createElement("input");
        inputJMBG.type = "number"
        inputJMBG.placeholder = "Jedinstveni maticni broj gradjana";
        let inputDatumRodjenja = document.createElement("input");
        inputDatumRodjenja.type = "date"
        inputDatumRodjenja.placeholder = "datum";
        let inputAdresa = document.createElement("input");
        inputAdresa.type = "text"
        inputAdresa.placeholder = "Adresa";
        
        let imeDiv = this.addRow(place);
        imeDiv.appendChild(lblIme);
        imeDiv.appendChild(inputIme);

        let prezimeDiv = this.addRow(place);
        prezimeDiv.appendChild(lblPrezime);
        prezimeDiv.appendChild(inputPrezime);

        let jmbgDiv = this.addRow(place);
        jmbgDiv.appendChild(lblJMBG);
        jmbgDiv.appendChild(inputJMBG);

        let datumRodjenjaDiv = this.addRow(place);
        datumRodjenjaDiv.appendChild(lblDatumRodjenja);
        datumRodjenjaDiv.appendChild(inputDatumRodjenja);

        let adresaDiv = this.addRow(place);
        adresaDiv.appendChild(lblAdresa);
        adresaDiv.appendChild(inputAdresa);
        this.klientContainer = newContainer;

        newContainer.appendChild(imeDiv);
        newContainer.appendChild(prezimeDiv);
        newContainer.appendChild(jmbgDiv);
        newContainer.appendChild(datumRodjenjaDiv);
        newContainer.appendChild(adresaDiv);


        let btnAdd = document.createElement("button");
        btnAdd.innerHTML = "Dodaj";
        btnAdd.className = "btnAdd"
        btnAdd.onclick = (ev)=> this.dodajKorisnika(inputIme.value,inputPrezime.value,inputAdresa.value,inputDatumRodjenja.value,inputJMBG.value)
        placeForBtn.appendChild(btnAdd);



        // if(inputIme.value === null || inputIme.value ===undefined || inputIme.value === "")
        // {
        //     alert("Unesite Vase Ime!");
        //     return;
        // }
        // if(inputPrezime.value === null || inputPrezime.value ===undefined || inputPrezime.value === "")
        // {
        //     alert("Unesite Vase Prezime");
        //     return;
        // }
        // if(inputAdresa.value === null || inputAdresa.value ===undefined || inputAdresa.value === "")
        // {
        //     alert("Unesite Vasu Adresu!");
        //     return;
        // }
        // if(inputDatumRodjenja.value.length != 13)
        // {
        //     alert("JMBG mora imati 13 cifara!");
        //     return;
        // }
        // if(inputDatumRodjenja.value)
        // btnAdd.onclick = (ev)=>
        // {
        //     fetch("https://localhost:5001/Korisnik/DodatiKorisnika/" + inputIme.value + "/"+ inputPrezime.value + "/"+ inputJMBG.value + "/" + inputDatumRodjenja.value + "/" + inputAdresa,
        //     {
        //         method: "POST"
        //     }).then(
        //         s=>
        //         {
        //             if(s.ok)
        //             {
        //                 this.korisnik = new Korisnik(p.id,p.ime,p.prezime,p.jmbg,p.datumRodjenja, p.adresa)
        //                 this.korisnik.drawInfoUser(place);

        //                 placeForBtn.removeChild(btnAdd);
        
        //                 let btnDodaj = document.createElement("button");
        //                 btnDodaj.innerHTML = "Dodaj klijenta";
        //                 btnDodaj.className = "btnDodajklijenta"
        //                 placeForBtn.appendChild(btnDodaj)
        //                 btnAdd.onclick = (ev)=>this.addkorisnik();
        //             }
        //         }
        //     );
        
        // }
        


    }

    dodajKorisnika(ime,prezime,adresa,datum,jmbg)
    {
        if(ime === null || ime ===undefined || ime === "")
        {
            alert("Unesite Vase Ime!");
            return;
        }
        if(adresa === null || adresa ===undefined ||adresa === "")
        {
            alert("Unesite Vase Prezime");
            return;
        }
        if(adresa === null || adresa ===undefined || adresa === "")
        {
            alert("Unesite Vasu Adresu!");
            return;
        }
        if(jmbg.length != 13)
        {
            alert("JMBG mora imati 13 cifara!");
            return;
        }
        
        {
            fetch("https://localhost:5001/Korisnik/DodatiKorisnika/" + ime + "/"+ prezime + "/"+ jmbg + "/" + datum + "/" + adresa,
            {
                method: "POST"
            }).then(
                s=>
                {
                    if(s.ok)
                    {
                        this.korisnik = new Korisnik(s.id,s.ime,s.prezime,s.jmbg,s.datumRodjenja, s.adresa)
                        this.deleteElementswith("clientInfoContainer")
                        let tmp = document.createElement("div")
                        this.klientContainer.appendChild(tmp);
                        this.klientContainer.className = "clientInfoContainer"
                        this.korisnik.drawInfoUser(tmp);

        
                        // let btnDodaj = document.createElement("button");
                        // btnDodaj.innerHTML = "Dodaj klijenta";
                        // btnDodaj.className = "btnDodajklijenta"
                        // placeForBtn.appendChild(btnDodaj)
                        // btnAdd.onclick = (ev)=>this.addkorisnik();
                    }
                }
            );
        
        }
    }


    addRow(place)
    {
        let rowDiv = document.createElement("div");
        place.appendChild(rowDiv);
        return rowDiv;
    }

    addVrsteRacuna(gde)
    {
        var lista = new Array();
        let se = document.createElement("select")
        se.className = "select"
        let op;
        op = document.createElement("option");
        op.innerHTML = "All"
         op.valut = 0
         se.appendChild(op)
        
            fetch("https://localhost:5001/VrstaRacuna/PreuzmiVrsteRacuna",
            {
                method: "GET"
            }).then(p=>
                {
                    p.json().then(vrste => 
                        {
                            vrste.forEach(element => {
                                var v = new VrsteRacuna(element.id,element.vrstaNaziv);
                                lista.push(v);
                                 op = document.createElement("option");
                                 op.innerHTML = v.naziv
                                 op.value = v.id
                                 se.appendChild(op)

                            });
                        })
                })
                gde.appendChild(se);
        
    }
    dodajFormu(gde,gdeUneti,btn)
    {
        let forma = document.createElement("div");
        forma.className = "forma"
        let lblBrojRacuna = document.createElement("label");
        lblBrojRacuna.innerHTML = "Broj Racuna:";
        forma.appendChild(lblBrojRacuna);

        let inputBrojRacuna = document.createElement("input");
        inputBrojRacuna.type = "number";
        inputBrojRacuna.placeholder = "Uneti broj novog racuna";
        forma.appendChild(inputBrojRacuna)

        let lblvaluta = document.createElement("label");
        lblvaluta.innerHTML = "Valuta:"
        forma.appendChild(lblvaluta)

        let inputValuta = document.createElement("input");
        inputValuta.type = "text";
        inputValuta.placeholder = "Uneti valutu novog racuna";
        forma.appendChild(inputValuta);

        let dateLbl = document.createElement("label");
        let currentDate = new Date()
        dateLbl.innerHTML = currentDate.toString();
        forma.appendChild(dateLbl);

        let btnDodajRacun = document.createElement("button");
        btnDodajRacun.innerHTML = "Dodaj novi racun"
        btnDodajRacun.onclick = (ev)=>this.dodajRacun(gdeUneti,inputBrojRacuna.value,inputValuta.value,currentDate.toDateString(),btn)
        forma.appendChild(btnDodajRacun);
        gde.appendChild(forma)


    }
    dodajRacun(gde,brojRacuna,valuta,date,btn)
    {
        

        if(brojRacuna === null || brojRacuna === "" || brojRacuna===undefined)
        {
            alert("Uneti broj racuna!")
            return;
        }
        if(valuta === null || valuta === "" || valuta===undefined)
        {
            alert("Uneti valutu!")
            return;
        }
        let se = document.querySelector(".select")
        console.log(se)
        let selected = se.options[se.selectedIndex].value;
        console.log(selected)

        fetch("https://localhost:5001/Racun/DodatiRacun/"+this.korisnikID+"/"+brojRacuna+"/"+valuta+"/" +date+ "/"+selected,
        {
            method: "POST"
        }).then(s=>
            {
                if(s.ok)
                {
                    this.deleteElementswith("forma")
                    this.deleteElementswith("racunHolder");
                    this.isapisatiRacune()
                }
            }

        )


    }
    isapisatiRacune()
    {
        let tmp = document.querySelector(".racuniInfoContainer")
        if(this.korisnikID !== -1)
        {
            fetch("https://localhost:5001/Racun/Racuni/"+ this.korisnikID,
            {
                method: "GET"
            }).then(r=>
                {
                    if(r.ok)
                    {
                        r.json().then(p=>{
                            p.forEach(data=>
                                {
                                    let racun = new Racun(data.id,data.broj,data.stanje,data.valuta, data.datumOtvaranja,data.datumZatvaranja , data.vrstaRacunaID);
                                    console.log(racun)
                                    racun.draw(tmp);
                                })
                        })
                    }

                })
        }
    }
    deleteElementswith(element)
    {
        var parent
        var tmp = document.querySelectorAll("."+element);
        tmp.forEach(p=>
            {
                parent = p.parentNode;
                parent.removeChild(p);
            })
        

       
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

    promeniRacun(broj,stanje,valuta,datumOtvaranja)
    {
        if(broj === null || broj === "" || broj===undefined)
        {
            alert("Uneti broj racuna!")
            return;
        }
        console.log(valuta)
        if(valuta === null || valuta === "" || valuta===undefined)
        {
            alert("Uneti valutu!")
            return;
        }
        if(stanje === null || stanje === "" || stanje===undefined)
        {
            alert("Uneti stanje!")
            return;
        }
        if(datumOtvaranja === null || datumOtvaranja === "" || datumOtvaranja===undefined)
        {
            alert("Uneti datum otvaranja!")
            return;
        }

        fetch("https://localhost:5001/Racun/PromenitiRacun/"+broj+"/"+valuta+"/"+stanje+"/"+datumOtvaranja,
        {
            method: "PUT"
        }).then(p=>
            {
                if(p.ok)
                {
                    this.deleteElementswith("formaIzmeni")
                    this.deleteElementswith("racunHolder");
                    this.isapisatiRacune()
                }
            }
        )


    }
    ZatvoriRacun(broj,datumZatvaranja)
    {
        if(broj === null || broj === "" || broj===undefined)
        {
            alert("Uneti broj racuna!")
            return;
        }
       
        if(datumZatvaranja === null || datumZatvaranja === "" || datumZatvaranja===undefined)
        {
            alert("Uneti datum otvaranja!")
            return;
        }

        fetch("https://localhost:5001/Racun/PromenitiRacun/"+broj+"/"+datumZatvaranja,
        {
            method: "PUT"
        }).then(p=>
            {
                if(p.ok)
                {
                    this.deleteElementswith("formaZatvori");
                    this.deleteElementswith("racunHolder");
                    this.isapisatiRacune()
                }
            }
        )


    }

    izbrisiRacun(brojRacuna)
    {
        
        if(brojRacuna === null || brojRacuna === "" || brojRacuna===undefined)
        {
            alert("Uneti broj racuna!")
            return;
        }

        fetch("https://localhost:5001/Racun/IzbrisiRacun/"+ brojRacuna,
        {
            method: "DELETE"
        }).then(
            p=>
            {
                if(p.ok)
                {
                    this.deleteElementswith("formaIzbrisi");
                    this.deleteElementswith("racunHolder");
                    this.isapisatiRacune()
                }
            }
        )
    }


    dodajIzbrisi(gde)
    {
        let forma = document.createElement("div");
        forma.className = "formaIzbrisi"
        let lblBrojRacuna = document.createElement("label");
        lblBrojRacuna.innerHTML = "Broj Racuna:";
        forma.appendChild(lblBrojRacuna);

        let inputBrojRacuna = document.createElement("input");
        inputBrojRacuna.type = "number";
        inputBrojRacuna.placeholder = "Uneti broj racuna";
        forma.appendChild(inputBrojRacuna)

        let btnIzbrisi = document.createElement("button");
        btnIzbrisi.innerHTML = "Izbrisi"
        btnIzbrisi.onclick = (ev)=>this.izbrisiRacun(inputBrojRacuna.value)
        forma.appendChild(btnIzbrisi);
        gde.appendChild(forma)
    }

}