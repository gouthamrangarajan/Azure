import "./style.css";
const useMsgsFromAzureFunction = () => {
  const fetchNew = async () => {
    let rwResp = await fetch("http://localhost:7071/api/Fn101");
    return await rwResp.text();
  };
  return { fetchNew };
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
    const { fetchNew } = useMsgsFromAzureFunction();
    const fetchData = async () => {
      const newDt = await fetchNew();
      fetchedData.value.push(newDt);
    };
    return { fetchedData, fetchData };
  },
});
vueApp.mount("#app");
