import axios from "axios";

class UserService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }

  postUserLogin(baseApi, body) {
    return axios.post(
      baseApi + "api/authentication",
      body
    );
  }

  getUserList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/user`, this.httpHeader(jwtToken));
  }

  postUserRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/user`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  putUserUpdate(baseApi, jwtToken, body) {
    return axios.put(
      baseApi + `api/user`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const UserServices = new UserService();
export { UserServices as default };
