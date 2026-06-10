public static class FechaHelper{
    public static string GetTrimestre(DateTime? fecha){
        if (!fecha.HasValue) return "-";

        var mes = fecha.Value.Month;

        if (mes >= 1 && mes <= 3) return "Q1";
        if (mes >= 4 && mes <= 6) return "Q2";
        if (mes >= 7 && mes <= 9) return "Q3";

        return "Q4";
    }
}
