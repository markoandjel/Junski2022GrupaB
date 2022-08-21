

export class Automobil
{
    constructor(idSpoj,marka,model,boja,cena,datum,kolicina)
    {
        this.idSpoj=idSpoj;
        this.marka=marka;
        this.model=model;
        this.boja=boja;
        this.cena=cena;
        this.datum=datum;
        this.kolicina=kolicina;
        this.kontejner=null;
    }

    crtaj(host)
    {
        this.kontejner=host;
        
        let divZaAuto=document.createElement("div");
        divZaAuto.classList.add("divZaAuto");
        divZaAuto.classList.add(`divZaAuto${this.idSpoj}`);
        this.kontejner.appendChild(divZaAuto);

        let divZaPodatke=document.createElement("div");
        divZaPodatke.className="divZaPodatke";
        divZaAuto.appendChild(divZaPodatke);

        let infoAuto=["Marka","Model","Boja","Kolicina","Datum","Cena"]
        infoAuto.forEach(el=>{
            let labeInfo=document.createElement("label");
            labeInfo.className=`label${el}`;
            labeInfo.innerHTML=`${el}: `
            divZaPodatke.appendChild(labeInfo);
        })
        var date=new Date(this.datum)

        let pom=divZaAuto.querySelectorAll("label")
        pom[0].innerHTML=pom[0].innerHTML+` ${this.marka}`
        pom[1].innerHTML=pom[1].innerHTML+` ${this.model}`
        pom[2].innerHTML=pom[2].innerHTML+` ${this.boja}`
        pom[3].innerHTML=pom[3].innerHTML+` ${this.kolicina}`
        pom[4].innerHTML=pom[4].innerHTML+` ${date.toLocaleDateString()}`
        pom[5].innerHTML=pom[5].innerHTML+` ${this.cena}`

        let divZaNaruci=document.createElement("div");
        divZaNaruci.className="divZaNaruci";
        divZaAuto.appendChild(divZaNaruci);

        let buttonNaruci=document.createElement("button");
        buttonNaruci.className="buttonNaruci";
        buttonNaruci.innerHTML="Naruči";
        buttonNaruci.addEventListener("click",()=>this.naruci())
        divZaNaruci.appendChild(buttonNaruci);

    }

    naruci()
    {
        fetch(`https://localhost:5002/Ispit/KupiAutomobil/${this.idSpoj}`,{method:"PUT"}).then(s=>{
            s.json().then(data=>{
                this.azuriraj(data);
            })
        })
    }

    azuriraj(data)
    {
        let divZaAuto=document.querySelector(`.divZaAuto${this.idSpoj}`);
        this.datum=data;
        this.kolicina--;
        if(this.kolicina==0)
        alert("Nema vise automobila");
        else{
            console.log(this.datum);
            let pom=divZaAuto.querySelector(".labelKolicina");
            pom.innerHTML=`Kolicina: ${this.kolicina}`;

            pom=divZaAuto.querySelector(".labelDatum");
            var date=new Date(this.datum)
            pom.innerHTML=`Datum poslednje kupovine: ${date.toLocaleDateString()}`;
        }
    }

}