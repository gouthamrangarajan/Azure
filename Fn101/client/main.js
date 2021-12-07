import "./style.css";
const useMsgsFromAzureFunction = () => {
  const isProgress = Vue.ref(false);
  const fetchNew = async () => {
    isProgress.value = true;
    let rwResp = await fetch("http://localhost:7071/api/Fn101");
    isProgress.value = false;
    return await rwResp.text();
  };
  return { fetchNew, isProgress };
};
const app = {
  setup() {
    return { welcomeMsg: "Welcome" };
  },
};

const vueApp = Vue.createApp(app);
vueApp.component("app-nav", {
  template: "#nav",
});
vueApp.component("fn-app-data", {
  template: "#fnAppData",
  setup() {
    const fetchedData = Vue.ref([]);
    const container = Vue.ref(null);
    const heightStyle = Vue.ref({});
    let timeout;
    const { fetchNew, isProgress } = useMsgsFromAzureFunction();
    const fetchData = async () => {
      const newDt = await fetchNew();
      fetchedData.value.push(newDt);
      setContainerHeight();
    };
    Vue.onMounted(() => {
      setContainerHeight();
    });
    Vue.onUnmounted(() => {
      if (timeout) clearTimeout(timeout);
    });
    const setContainerHeight = () => {
      timeout = setTimeout(() => {
        if (container.value) {
          const { height } =
            container.value.children[0].getBoundingClientRect();
          heightStyle.value = { height: height + "px" };
        }
      }, 100);
    };
    return { fetchedData, fetchData, container, heightStyle, isProgress };
  },
});
vueApp.mount("#app");
