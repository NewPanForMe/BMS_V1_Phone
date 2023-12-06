import { Message } from 'tdesign-mobile-vue';

const showMessage = (theme, content) => {
  if (Message[theme]) {
    Message[theme]({
      offset: [10, 16],
      content,
      duration: 2000,
      icon: true,
      zIndex: 20000,
      context: document.querySelector('.showMessage'),
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



const showError = (content) => showMessage('error',content);
const showInfo = (content) => showMessage('info', content);
const showWarning = (content) => showMessage('warning', content);
const showSuccess = (content) => showMessage('success', content);

export default { showWarning, showSuccess, showError, showInfo, getCurrentDate }