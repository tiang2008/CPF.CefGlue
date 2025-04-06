using CPF.CefGlue.Interop;

namespace CPF.CefGlue;

/// <summary>
///     Class representing SSL information.
/// </summary>
public sealed unsafe partial class CefSslInfo
{
    /// <summary>
    ///     Returns a bitmask containing any and all problems verifying the server
    ///     certificate.
    /// </summary>
    public CefCertStatus CertStatus => cef_sslinfo_t.get_cert_status(_self);

    /// <summary>
    ///     Returns the X.509 certificate.
    /// </summary>
    public CefX509Certificate GetX509Certificate()
    {
        return CefX509Certificate.FromNative(
            cef_sslinfo_t.get_x509_certificate(_self)
        );
    }
}