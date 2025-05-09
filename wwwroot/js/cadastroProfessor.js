const materias = {
    fundamental1: ["Português", "Matemática", "História", "Geografia", "Ciências"],
    fundamental2: ["Português", "Matemática", "Inglês", "História", "Geografia", "Ciências", "Artes"],
    medio: ["Física", "Química", "Biologia", "Literatura", "Redação", "Filosofia", "Sociologia"]
  };

  function atualizarMaterias() {
    const tipo = document.getElementById("tipoMateria").value;
    const container = document.getElementById("materiasCheckboxes");
    container.innerHTML = "";

    if (materias[tipo]) {
      materias[tipo].forEach((disciplina, index) => {
        const checkbox = document.createElement("div");
        checkbox.className = "form-check";
        checkbox.innerHTML = `
          <input class="form-check-input" type="checkbox" id="disciplina${index}" value="${disciplina}">
          <label class="form-check-label" for="disciplina${index}">${disciplina}</label>
        `;
        container.appendChild(checkbox);
      });
    }
  }

  const tabs = document.querySelectorAll('#navTabs .nav-link');
  tabs.forEach(tab => {
    tab.addEventListener('click', (e) => {
      e.preventDefault();
      tabs.forEach(t => t.classList.remove('active'));
      tab.classList.add('active');
    });
  });