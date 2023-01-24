import { MyHttpClient } from "my-http-client";
import { autoinject } from 'aurelia-framework';
import { v4 as uuidv4 } from 'uuid';
import { notDeepEqual } from "assert";
import { RouterConfiguration, Router } from 'aurelia-router';
import { IItem, IVenda } from "interfaces";



@autoinject
export class Registro {
  produtoAdd: string;

  venda = {} as IVenda;

  error: string;

  saveSuccessful: boolean;

  constructor(
    private httpClient: MyHttpClient, private router: Router
  ) { }

  addTextbox() {
    if (this.produtoAdd && this.produtoAdd.length) {
      if (!this.venda.itens) {
        this.venda.itens = [];
      }
      this.venda.itens.push({
        idItem: uuidv4(), descricao: this.produtoAdd
      });
      this.produtoAdd = null;
    }
  }

  removeTextbox(item: IItem) {
    const index = this.venda.itens.indexOf(item);
    this.venda.itens.splice(index, 1);
  }

  cancelForm() {
    this.venda = {} as IVenda;

  }

  async saveForm() {
    try {
      let resposta = await this.httpClient.postVenda(this.venda);
      if (resposta.statusCode == 201) {
        this.venda = resposta.result;
        this.error = null;
        this.saveSuccessful = true;
      } else {
        this.saveSuccessful = false;
        this.error = resposta.message;
      }

    } catch {

      this.error = "Não foi possível efetuar a compra"

    }

  }

  public voltarBusca() {
    this.router.navigateToRoute('buscar');
  }
}

