import axios from "axios";

class UserService {
  httpHeader(jwtToken) {
    return {
      headers: {
        Authorization: "Bearer " + jwtToken,
      },
    };
  }

  postUserLogin(baseApi, username, password) {
    return axios.post(
      baseApi + "api/user/login?username=" + username + "&password=" + password
    );
  }

  getUserList(baseApi, jwtToken) {
    return axios.get(baseApi + `api/v1/user/list`, this.httpHeader(jwtToken));
  }

  postUserRegister(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/user/register`,
      body,
      this.httpHeader(jwtToken)
    );
  }

  postUserUpdate(baseApi, jwtToken, body) {
    return axios.post(
      baseApi + `api/user/update`,
      body,
      this.httpHeader(jwtToken)
    );
  }
}
const UserServices = new UserService();
export { UserServices as default };
