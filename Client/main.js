import {Prodavnica} from "./Prodavnica.js"

fetch("https://localhost:5002/Ispit/PreuzmiProdavnice",{method:"GET"})
.then(s=>{
    s.json().then(data=>{
        data.forEach(el => {
            var prodavnica = new Prodavnica(el.id,el.naziv);
            prodavnica.crtaj(document.body);
        });
    })
})

export function obrisiDecicu(parent)
{
    while(parent.firstChild)
    {
        parent.removeChild(parent.firstChild);
    }
}
