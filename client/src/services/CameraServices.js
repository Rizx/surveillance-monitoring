import axios from "axios";

class CameraService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }
  
  getCameraStreaming(baseApi, params, jwtToken) {
    return axios.get(
      baseApi + `api/camera/streaming` + params,
      this.httpHeader(jwtToken)
    );
  }

  getCameraCameraList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/camera/videos`, this.httpHeader(jwtToken));
  }

  getCameraList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/camera`, this.httpHeader(jwtToken));
  }

  postCameraRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/camera/register`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  postCameraUpdate(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/camera/update`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const CameraServices = new CameraService();
export { CameraServices as default };
