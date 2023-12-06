import cookies from "vue-cookies";
const tokenName = "AccessToken";
const jwtVersion = "JwtVersion";
const refreshToken = "RefreshToken";
const userName = "Name";
const userCode = "UserCode";
const account = "Account";
cookies.config("20min")
const cookie = {
    saveToken(resp) {
        cookies.set(account, resp.account);
        cookies.set(tokenName, resp.token);
        cookies.set(jwtVersion, resp.jwtVersion);
        cookies.set(refreshToken,  resp.refreshToken);
        cookies.set(userName,  resp.name);
        cookies.set(userCode,  resp.code);
    },
    getToken() {
        var param = {
            tokenName: cookies.get(tokenName),
            jwtVersion: cookies.get(jwtVersion),
            refreshToken: cookies.get(refreshToken),
            userName: cookies.get(userName),
            userCode: cookies.get(userCode),
            account: cookies.get(account),
        };
        return param;
    },
    getRefreshToken() {
        return cookies.get(refreshToken);
    },
    getUserCode() {
        return cookies.get(userCode);
    },
    removeToken() {
        cookies.remove(tokenName);
        cookies.remove(jwtVersion);
        cookies.remove(refreshToken);
        cookies.remove(userName);
        cookies.remove(userCode);
        cookies.remove(account);
    },
    removeRefreshToken() {
        cookies.remove(refreshToken);
    },
};
export default cookie;
