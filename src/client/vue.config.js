const appsettings = require("./public/appsettings.json");
module.exports = {
  publicPath:
    process.env.NODE_ENV === "production" ? appsettings.PUBLIC_PATH : "/",
};
