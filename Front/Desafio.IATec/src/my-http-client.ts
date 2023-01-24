import { HttpClient } from 'aurelia-http-client';
import { autoinject } from 'aurelia-framework';

@autoinject
export class MyHttpClient {

  private http: HttpClient;
  constructor(http: HttpClient) {
    this.http = http;
  }

  postVenda(venda) {

    return this.http.post('https://localhost:44322/v1/vendas', venda)
      .then(response => {
        // handle the response
        return response.content;
      })
      .catch(error => {
        // handle the error
        throw JSON.parse(error.response);
      });
  }

  getVenda(id) {
    return this.http.get(`https://localhost:44322/v1/vendas/${id}`)
      .then(response => {
        // handle the response
        return response.content;
      })
      .catch(error => {
        // handle the error
        throw JSON.parse(error.response);
      });
  }

  putVenda(venda) {
    return this.http.put('https://localhost:44322/v1/vendas', venda)
      .then(response => {
        // handle the response
        return response.content;
      })
      .catch(error => {
        // handle the error
        return JSON.parse(error.response);
      });
  }
}
