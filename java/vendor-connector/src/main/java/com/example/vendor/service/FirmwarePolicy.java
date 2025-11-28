package com.example.vendor.service;
public class FirmwarePolicy {
  public static boolean canUpdate(String nowLocal, String window) {
    String[] p = window.split("-");
    java.time.LocalTime from = java.time.LocalTime.parse(p[0]);
    java.time.LocalTime to   = java.time.LocalTime.parse(p[1]);
    java.time.LocalTime now  = java.time.LocalTime.parse(nowLocal);
    if (!from.isAfter(to)) return !now.isBefore(from) && !now.isAfter(to);
    return !now.isBefore(from) || !now.isAfter(to);
  }
  public static int semverCmp(String a, String b) {
    String[] pa = a.split("\\.");
    String[] pb = b.split("\\.");
    for (int i=0;i<3;i++){
      int ai = Integer.parseInt(pa[i]);
      int bi = Integer.parseInt(pb[i]);
      if (ai!=bi) return Integer.compare(ai, bi);
    }
    return 0;
  }
}
