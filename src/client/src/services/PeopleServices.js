import axios from "axios";

class WargaService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }

  getWargaList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/member`, this.httpHeader(jwtToken));
  }

  postCardRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/member/card`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  postWargaRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/member`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  putWargaUpdate(baseApi, jwtToken, body) {
    return axios.put(
      baseApi + `api/member`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const WargaServices = new WargaService();
export { WargaServices as default };
