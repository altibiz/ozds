export function scrollIndexIntoView(wrap, index, itemSize) {
  if (!wrap) {
    return;
  }
  const targetTop = index * itemSize;
  const targetBottom = targetTop + itemSize;
  const viewTop = wrap.scrollTop;
  const viewBottom = viewTop + wrap.clientHeight;
  if (targetTop < viewTop) {
    wrap.scrollTop = targetTop;
  } else if (targetBottom > viewBottom) {
    wrap.scrollTop = targetBottom - wrap.clientHeight;
  }
}
