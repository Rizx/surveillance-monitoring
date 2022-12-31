import axios from "axios";

class PeopleService {
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
      baseApi + `api/member`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const PeopleServices = new PeopleService();
export { PeopleServices as default };
