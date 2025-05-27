const apiUrl = "http://localhost:5001/api/auth/login"; // corrigido: rota completa da sua API

function login() {
  const email = document.getElementById("email").value;
  const senha = document.getElementById("password").value;

  fetch(apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      UserName: email,
      Password: senha
    })
  })
    .then((res) => {
      if (!res.ok) throw new Error("Erro ao autenticar");
      return res.json();
    })
    .then((data) => {
      localStorage.setItem("token", data.token);
      window.location.href = "dashboard.html";
    })
    .catch(() => alert("E-mail ou senha inválidos"));
}
