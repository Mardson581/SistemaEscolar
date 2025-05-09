const tabs = document.querySelectorAll('#navTabs .nav-link');

tabs.forEach(tab => {
  tab.addEventListener('click', (e) => {
    e.preventDefault(); // Prevent page jump
    tabs.forEach(t => t.classList.remove('active'));
    tab.classList.add('active');
  });
});