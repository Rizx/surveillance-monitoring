import axios from "axios";

class WargaService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }

  getTransactionList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/history`, this.httpHeader(jwtToken));
  }

  postTransactionRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/transaction/register`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const WargaServices = new WargaService();
export { WargaServices as default };
