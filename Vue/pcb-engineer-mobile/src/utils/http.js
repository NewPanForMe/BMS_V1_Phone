//通讯组件
import axios from "axios";
import router from '../router/index'
import cookie from "./cookies";
import { Message } from 'tdesign-mobile-vue';


const showMessage = (theme, content) => {
    if (Message['error']) {
        Message[theme]({
            offset: [10, 16],
            content,
            duration: 3000,
            icon: true,
            zIndex: 20000,
            context: document.querySelector('.showMessage'),
        });
    }
};

const showError = (content) => showMessage('error', content);


const instance = axios.create({
    timeout: 10000
});
if (window.server.isDev) {
    instance.defaults.baseURL = window.server.baseDevUrl
} else {
    instance.defaults.baseURL = window.server.baseUrl
}



//统一设置post请求头
instance.defaults.headers.post["Content-Type"] = "application/json";
instance.defaults.headers.get["Content-Type"] = "application/json";

//添加请求拦截器
instance.interceptors.request.use(
    (config) => {
        //判断cookie是否存在
        var token = cookie.getToken();
        if (token) {
            //存在,放入请求头
            config.headers.Authorization = "Bearer " + token.tokenName;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);
//添加resp拦截器
instance.interceptors.response.use(
    (resp) => {
        //如果返回的结果为true
        if (resp.data != null) {
            return resp.data;
        }
    },
    (error) => {
        if (error.response.status == 401) {
            showError("无权限，请重新登录")
            cookie.removeToken();
            router.push({ path: "/login" });
        }
        return Promise.reject(error);
    }
);
export default { instance };
