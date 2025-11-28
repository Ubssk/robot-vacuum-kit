export function canUpdate(nowLocal, window){
  const [from, to] = window.split("-");
  const [nh, nm] = nowLocal.split(":").map(Number);
  const [fh, fm] = from.split(":").map(Number);
  const [th, tm] = to.split(":").map(Number);
  const now = nh*60+nm, f = fh*60+fm, t = th*60+tm;
  if (f <= t) return now >= f && now <= t;
  return now >= f || now <= t;
}
export function semverCmp(a,b){
  const pa=a.split('.').map(Number), pb=b.split('.').map(Number);
  for (let i=0;i<3;i++){ if (pa[i]!==pb[i]) return pa[i]-pb[i]; }
  return 0;
}
