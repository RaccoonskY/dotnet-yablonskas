using System.ComponentModel.DataAnnotations;

namespace Dotnet.Web.Dto;

public class EquationSolveRequestDto
{
    [Required]
    public double XFactor;
    
    [Required]
    public double YFactor;
    
    [Required]
    public double ZFactor;
}