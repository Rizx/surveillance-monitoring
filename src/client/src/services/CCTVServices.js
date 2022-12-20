import axios from "axios";

class CCTVService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }

  getCCTVCameraList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/v1/cctv/camera`, this.httpHeader(jwtToken));
  }

  getCCTVList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/v1/cctv/list`, this.httpHeader(jwtToken));
  }

  postCCTVRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/cctv/register`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  postCCTVUpdate(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/cctv/update`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const CCTVServices = new CCTVService();
export { CCTVServices as default };
