### Vite and Vue client

```javascript
const useMsgsFromAzureFunction = () => {
  const fetchNew = async () => {
    let rwResp = await fetch("http://localhost:7071/api/Fn101");
    return await rwResp.text();
  };
  return { fetchNew };
};
```
