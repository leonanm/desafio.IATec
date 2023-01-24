import { IItem, IVenda, StatusVenda } from './../../interfaces';
import { HttpClient } from 'aurelia-http-client';
import { autoinject } from 'aurelia-framework';
import { MyHttpClient } from "my-http-client";
import { RouterConfiguration, Router } from 'aurelia-router';



@autoinject
export class Buscar {
  idVenda: string;
  venda: IVenda;
  error: string;  
  status: string;
  saveSuccessful: boolean;
  constructor(private http: MyHttpClient, private router: Router) {

  }


  async searchVenda() {
    let resposta = await this.http.getVenda(this.idVenda)
    if (resposta.statusCode == 200) {
      this.venda = resposta.result;
      this.status = this.venda.status.toString();
      this.error = null;
    } else {
      this.error = resposta.message;
    }

  }

  public novaCompra(){ 
    if(this.router)
    this.router.navigateToRoute('registro');
  }

  public async alterarStatus(){
    this.venda.status = parseInt(this.status);
    let resposta = await this.http.putVenda(this.venda);
    if (resposta.statusCode == 200) {
      this.venda = resposta.result;
      this.error = null;
      this.saveSuccessful = true;
    } else {
      this.error = resposta.message;
      this.saveSuccessful = false;
    }

  }

  public pegarNomestatus(){
    return StatusVenda[this.venda.status];
  }

}



