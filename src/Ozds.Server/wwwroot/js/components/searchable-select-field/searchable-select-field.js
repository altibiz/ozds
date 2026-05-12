export function scrollItemIntoView(id) {
  document.getElementById(id)?.scrollIntoView({
    block: 'nearest',
    behavior: 'instant'
  });
}
