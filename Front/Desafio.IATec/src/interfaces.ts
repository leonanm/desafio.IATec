export interface IVenda {
  idVenda: string;
  vendedor: IVendedor;
  dataVenda: string;
  itens: IItem[];
  status: StatusVenda;

}

export interface IVendedor {
  idVendedor: string;
  nome: string;
  cpf: string;
}

export interface IItem {
  idItem: string;
  descricao: string;
}

export enum StatusVenda {
  AguardandoPagamento,
  PagamentoAprovado,
  Enviado,
  Entregue,
  Cancelado
}
