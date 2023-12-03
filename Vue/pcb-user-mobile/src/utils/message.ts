import { Message } from 'tdesign-mobile-vue';

const showMessage = (theme: string, content: string) => {
  if (Message['error']) {
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

const showSuccess = (content: string) => showMessage('success', content);
const showError = (content: string) => showMessage('error', content);
const showWarning = (content: string) => showMessage('warning', content);
const showInfo = (content: string) => showMessage('info', content);

export default { showWarning, showSuccess, showError, showInfo }