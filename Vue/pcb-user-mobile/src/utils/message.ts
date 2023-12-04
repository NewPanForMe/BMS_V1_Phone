import { Message } from 'tdesign-mobile-vue';

const showMessage = (theme: string, content: string) => {
  if(  Message[theme]){
    Message[theme]({
      offset: [10, 16],
      content,
      duration: 5000,
      icon: true,
      zIndex: 20000,
      context: document.querySelector('.button-demo'),
    });
  }

};

//获取当前日期
const getCurrentDate = () => {
  let now = new Date();
  let year = now.getFullYear();
  let month = now.getMonth() + 1;
  let day = now.getDate();
  return year + "-" + month + "-" + day;
}





const showSuccess = (content: string) => showMessage('success', content);
const showError = (content: string) => showMessage('error', content);
const showWarning = (content: string) => showMessage('warning', content);
const showInfo = (content: string) => showMessage('info', content);

export default { showWarning, showSuccess, showError, showInfo ,getCurrentDate}