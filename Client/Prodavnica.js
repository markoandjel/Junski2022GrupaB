import { obrisiDecicu } from "./main.js"

import { Automobil } from "./Automobil.js";

export class Prodavnica
{
    constructor(id,naziv)
    {
        this.id=id;
        this.naziv=naziv;
        this.kontejner=null;
        this.idMarka=null;
        this.idModel=null;
        this.idBoja=null;
    }

    crtaj(host)
    {
        this.kontejner=host;

        let divZaProdavnicu=document.createElement("div");
        divZaProdavnicu.classList.add(`divZaProdavnicu`);
        divZaProdavnicu.classList.add(`divZaProdavnicu${this.id}`);
        divZaProdavnicu.innerHTML=this.naziv;
        this.kontejner.appendChild(divZaProdavnicu);

        let divZaPrikaz=document.createElement("div");
        divZaPrikaz.className="divZaPrikaz";
        divZaProdavnicu.appendChild(divZaPrikaz);

        let divZaFiltere=document.createElement("div");
        divZaFiltere.className="divZaFiltere";
        divZaPrikaz.appendChild(divZaFiltere);



        let divZaAutomobile=document.createElement("div");
        divZaAutomobile.classList.add("divZaAutomobile");
        divZaAutomobile.classList.add(`divZaAutomobile${this.id}`);
        divZaPrikaz.appendChild(divZaAutomobile);
        

        let tataFiltera=document.createElement("div");
        tataFiltera.className="tataFiltera";
        divZaFiltere.appendChild(tataFiltera);

        let divZaLabele = document.createElement("div");
        divZaLabele.className="divZaLabele";
        tataFiltera.appendChild(divZaLabele);

        let nizLabelaZaFiltere=["Marka","Model","Boja"]
        nizLabelaZaFiltere.forEach(p=>{
            let labela=document.createElement("label");
            labela.innerHTML=p+":";
            labela.className="FilterLabela";
            divZaLabele.appendChild(labela);
        })

        let divZaOpcije = document.createElement("div");
        divZaOpcije.className="divZaOpcije";
        tataFiltera.appendChild(divZaOpcije);

        nizLabelaZaFiltere.forEach(p=>{
            let select = document.createElement("select");
            select.className=`select${p}${this.id}`;
            divZaOpcije.appendChild(select);
        })

        let divZaDugme = document.createElement("div");
        divZaDugme.className="divZaDugme";
        divZaFiltere.appendChild(divZaDugme);

        let buttonPronadji = document.createElement("button");
        buttonPronadji.className="buttonPronadji";
        buttonPronadji.innerHTML="Pronadji"
        buttonPronadji.addEventListener("click",()=>this.pronadji());
        divZaDugme.appendChild(buttonPronadji);

        //dodavanje Event Listenera
        let pomMarka = divZaOpcije.querySelector(`.selectMarka${this.id}`);
        pomMarka.addEventListener("click",()=>this.prikazModela(pomMarka,pomModel))

        let pomModel = document.querySelector(`.selectModel${this.id}`);
        pomModel.addEventListener("click",()=>this.prikazBoja(pomMarka.value,pomModel.value))

        let pomBoja = document.querySelector(`.selectBoja${this.id}`);
        pomBoja.addEventListener("click",()=>this.idBoja=pomBoja.value)
        //deo za fetch
        

        var listaMaraka=[];
        fetch(`https://localhost:5002/Ispit/PreuzmiMarke/${this.id}`,{method:"GET"}).then(s=>{
            s.json().then(data=>{
                data.forEach(el=>{
                    const isDuplicate=listaMaraka.includes(el.id);
                    if(!isDuplicate)
                    {
                        listaMaraka.push(el.id);
                        let opcija=document.createElement("option");
                        opcija.value=el.id;
                        opcija.innerHTML=el.naziv;
                        pomMarka.appendChild(opcija);
                    }
                })
                this.idMarka=listaMaraka[0];
            })
        })
    }   

    prikazModela(pomMarka,pomModel)
    {
        this.idMarka=pomMarka.value;
        pomModel.replaceChildren();
        let pomBoja = document.querySelector(`.selectBoja${this.id}`);
        if(pomBoja!=null)pomBoja.replaceChildren();
        var listaModela=[];
        fetch(`https://localhost:5002/Ispit/PreuzmiModele/${this.id}/${this.idMarka}`,{method:"GET"}).then(s=>{
            s.json().then(data=>{
                let opcija=document.createElement("option");
                opcija.innerHTML="-";
                opcija.value=0;
                pomModel.appendChild(opcija);
                data.forEach(el=>{
                    const isDuplicate=listaModela.includes(el.id);
                    if(!isDuplicate)
                    {
                        listaModela.push(el.id);
                        opcija=document.createElement("option");
                        opcija.value=el.id;
                        opcija.innerHTML=el.naziv;
                        pomModel.appendChild(opcija);
                    }
                })
            })
        })
    }
    prikazBoja(idMarka,idModel)
    {
        this.idModel=idModel;
        let pomBoja = document.querySelector(`.selectBoja${this.id}`);
        pomBoja.replaceChildren();
        var listaBoja=[];
        fetch(`https://localhost:5002/Ispit/PreuzmiBoje/${this.id}/${idMarka}/${idModel}`,{method:"GET"}).then(s=>{
            s.json().then(data=>{
                let opcija=document.createElement("option");
                opcija.innerHTML="-";
                opcija.value=0;
                pomBoja.appendChild(opcija);
                data.forEach(el=>{
                    const isDuplicate=listaBoja.includes(el.id);
                    if(!isDuplicate)
                    {
                        listaBoja.push(el.id);
                        opcija=document.createElement("option");
                        opcija.value=el.id;
                        opcija.innerHTML=el.naziv;
                        pomBoja.appendChild(opcija);
                    }
                })
            })
        })
    }

    pronadji()
    {
        let t=document.querySelector(`.divZaAutomobile${this.id}`);
        if(t!=null)
        {
            obrisiDecicu(t)
            
        }         
        if(this.idModel==null && this.idBoja==null)
        {
            fetch(`https://localhost:5002/Ispit/PreuzmiAutomobile/${this.id}/${this.idMarka}`,{method:"GET"}).then(s=>{
                s.json().then(data=>{
                    data.forEach(el=>{
                        let auto=new Automobil(el.id,el.marka,el.model,el.boja,el.cena,el.datum,el.kolicina)
                        auto.crtaj(document.querySelector(`.divZaAutomobile${this.id}`))
                    })
                    
                })
            })
        }
        else if(this.idModel!=null)
        {
            if(this.idBoja!=null)
            {
                fetch(`https://localhost:5002/Ispit/PreuzmiAutomobile/${this.id}/${this.idMarka}?idModel=${this.idModel}&idBoja=${this.idBoja}`,{method:"GET"}).then(s=>{
                s.json().then(data=>{
                    data.forEach(el=>{
                        let auto=new Automobil(el.id,el.marka,el.model,el.boja,el.cena,el.datum,el.kolicina)
                        auto.crtaj(document.querySelector(`.divZaAutomobile${this.id}`))
                    })
                    this.idBoja=null;
                    this.idModel=null;
                })
                })
                
            }else
            {
                fetch(`https://localhost:5002/Ispit/PreuzmiAutomobile/${this.id}/${this.idMarka}?idModel=${this.idModel}`,{method:"GET"}).then(s=>{
                s.json().then(data=>{
                    data.forEach(el=>{
                        let auto=new Automobil(el.id,el.marka,el.model,el.boja,el.cena,el.datum,el.kolicina)
                        auto.crtaj(document.querySelector(`.divZaAutomobile${this.id}`))
                    })
                    this.idModel=null;
                    this.idBoja=null;
                })
            })
            }
        }
    }


}