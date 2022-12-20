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
    return axios.get(baseApi + `api/v1/warga/list`, this.httpHeader(jwtToken));
  }

  postWargaRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/warga/register`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  postWargaUpdate(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/warga/update`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const WargaServices = new WargaService();
export { WargaServices as default };
