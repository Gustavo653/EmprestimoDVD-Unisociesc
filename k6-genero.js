import http from "k6/http";
import { check } from "k6";

export const options = {
  stages: [
    { duration: "1m", target: 300 },
    { duration: "3m", target: 600 },
    { duration: "1m", target: 0 },
  ],
  thresholds: {
    http_req_duration: ["p(99)<1500"],
  },
};

export function generoCreate() {
  const url = `http://localhost:5001/api/Genero/CreateOrUpdate`;
  const names = ["João", "Maria", "Pedro", "Ana", "Lucas"];
  const randomIndex = Math.floor(Math.random() * names.length);
  const payload = {
    descricao: names[randomIndex],
  };
  const headers = { "Content-Type": "application/json" };
  const response = http.post(url, JSON.stringify(payload), {
    headers: headers,
  });
  check(response, {
    "response status is 200": (r) => r.status === 200,
  });
  console.log(
    `VU ${__VU}: Genero criado com sucesso. Status: ${response.status}, Tempo de resposta: ${response.timings.duration}ms`
  );
  return response.json;
}

export function generoGetAll() {
  const url = `http://localhost:5001/api/Genero/GetAll`;
  const payload = {};
  const headers = {};
  const response = http.get(url, payload, { headers: headers });
  check(response, {
    "response status is 200": (r) => r.status === 200,
  });
  console.log(
    `VU ${__VU}: Todos os generos foram listados com sucesso. Status: ${response.status}, Tempo de resposta: ${response.timings.duration}ms`
  );
  return response.json;
}

export default function setup() {
  generoCreate();
//   generoGetAll();
  return 0;
}
