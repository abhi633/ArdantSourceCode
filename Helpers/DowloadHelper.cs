﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArdantOffical.Helpers
{
    public class DownloadHelper
    {
        private readonly IWebHostEnvironment env;
        //public DownloadHelper(IWebHostEnvironment env)
        //{
        //    this.env = env;
        //}
        public async Task DownloadAsync(string path, IJSRuntime jSRuntime)
        {
            var file = Get(path);
            await jSRuntime.InvokeVoidAsync("downloadBase64File", file.ContentType, file.FileStream, file.FileDownloadName);
        }
        /// <summary>
        /// file download
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>

        public FileRes Get(string path)
        {
            FileRes res = new FileRes();
            byte[] buffer = System.IO.File.ReadAllBytes(path);
            var stream = new MemoryStream(buffer);
            MimeTypes type;
            Enum.TryParse(path.Split("//").LastOrDefault().Split('.')[1], out type);
            string contentType = EnumerationExtension.Description(type);
            res.FileStream = Convert.ToBase64String(buffer);
            res.ContentType = contentType;
            res.FileDownloadName = path.Split("//").LastOrDefault();
            return res;
        }
    }
    public static class EnumerationExtension
    {
        public static string Description(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }
            // return description
            return displayAttribute?.Description ?? "Description Not Found";
        }
    }
    public class FileRes
    {
        public string FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileDownloadName { get; set; }
    }

    public class Media
    {
        public Media()
        {

        }
        public Media(string Path)
        {
            Name = Path.Split("\\").LastOrDefault();
            this.Path = Path.Replace('\\', '^');
        }

        public string Name { get; set; }
        public string Path { get; set; }
    }

    public enum MimeTypes
    {
        [Description("application/postscript")]
        ai,
        [Description("audio/x-aiff")]
        aif,
        [Description("audio/x-aiff")]
        aifc,
        [Description("audio/x-aiff")]
        aiff,
        [Description("text/plain")]
        asc,
        [Description("application/atom+xml")]
        atom,
        [Description("audio/basic")]
        au,
        [Description("video/x-msvideo")]
        avi,
        [Description("application/x-bcpio")]
        bcpio,
        [Description("application/octet-stream")]
        bin,
        [Description("image/bmp")]
        bmp,
        [Description("application/x-netcdf")]
        cdf,
        [Description("image/cgm")]
        cgm,
        [Description("application/octet-stream")]
        cpio,
        [Description("application/mac-compactpro")]
        cpt,
        [Description("application/x-csh")]
        csh,
        [Description("text/css")]
        css,
        [Description("application/x-director")]
        dcr,
        [Description("video/x-dv")]
        dif,
        [Description("application/x-director")]
        dir,
        [Description("image/vnd.djvu")]
        djv,
        [Description("image/vnd.djvu")]
        djvu,
        [Description("application/octet-stream")]
        dll,
        [Description("application/octet-stream")]
        dmg,
        [Description("application/octet-stream")]
        dms,
        [Description("application/msword")]
        doc,
        [Description("application/xml-dtd")]
        dtd,
        [Description("video/x-dv")]
        dv,
        [Description("application/x-dvi")]
        dvi,
        [Description("application/x-director")]
        dxr,
        [Description("application/postscript")]
        eps,
        [Description("text/x-setext")]
        etx,
        [Description("application/octet-stream")]
        exe,
        [Description("application/andrew-inset")]
        ez,
        [Description("image/gif")]
        gif,
        [Description("application/srgs")]
        gram,
        [Description("application/srgs+xml")]
        grxml,
        [Description("application/x-gtar")]
        gtar,
        [Description("application/x-hdf")]
        hdf,
        [Description("application/mac-binhex40")]
        hqx,
        [Description("text/html")]
        htm,
        [Description("text/html")]
        html,
        [Description("x-conference/x-cooltalk")]
        ice,
        [Description("image/x-icon")]
        ico,
        [Description("text/calendar")]
        ics,
        [Description("image/ief")]
        ief,
        [Description("text/calendar")]
        ifb,
        [Description("model/iges")]
        iges,
        [Description("model/iges")]
        igs,
        [Description("application/x-java-jnlp-file")]
        jnlp,
        [Description("image/jp2")]
        jp2,
        [Description("image/jpeg")]
        jpe,
        [Description("image/jpeg")]
        jpeg,
        [Description("image/jpeg")]
        jpg,
        [Description("application/x-javascript")]
        js,
        [Description("audio/midi")]
        kar,
        [Description("application/x-latex")]
        latex,
        [Description("application/octet-stream")]
        lha,
        [Description("application/octet-stream")]
        lzh,
        [Description("audio/x-mpegurl")]
        m3u,
        [Description("audio/mp4a-latm")]
        m4a,
        [Description("audio/mp4a-latm")]
        m4b,
        [Description("audio/mp4a-latm")]
        m4p,
        [Description("video/vnd.mpegurl")]
        m4u,
        [Description("video/x-m4v")]
        m4v,
        [Description("image/x-macpaint")]
        mac,
        [Description("application/x-troff-man")]
        man,
        [Description("application/mathml+xml")]
        mathml,
        [Description("application/x-troff-me")]
        me,
        [Description("model/mesh")]
        mesh,
        [Description("audio/midi")]
        mid,
        [Description("audio/midi")]
        midi,
        [Description("application/vnd.mif")]
        mif,
        [Description("video/quicktime")]
        mov,
        [Description("video/x-sgi-movie")]
        movie,
        [Description("audio/mpeg")]
        mp2,
        [Description("audio/mpeg")]
        mp3,
        [Description("video/mp4")]
        mp4,
        [Description("video/mpeg")]
        mpe,
        [Description("video/mpeg")]
        mpeg,
        [Description("video/mpeg")]
        mpg,
        [Description("audio/mpeg")]
        mpga,
        [Description("application/x-troff-ms")]
        ms,
        [Description("model/mesh")]
        msh,
        [Description("video/vnd.mpegurl")]
        mxu,
        [Description("application/x-netcdf")]
        nc,
        [Description("application/oda")]
        oda,
        [Description("application/ogg")]
        ogg,
        [Description("image/x-portable-bitmap")]
        pbm,
        [Description("image/pict")]
        pct,
        [Description("chemical/x-pdb")]
        pdb,
        [Description("application/pdf")]
        pdf,
        [Description("image/x-portable-graymap")]
        pgm,
        [Description("application/x-chess-pgn")]
        pgn,
        [Description("image/pict")]
        pic,
        [Description("image/pict")]
        pict,
        [Description("image/png")]
        png,
        [Description("image/x-portable-anymap")]
        pnm,
        [Description("image/x-macpaint")]
        pnt,
        [Description("image/x-macpaint")]
        pntg,
        [Description("image/x-portable-pixmap")]
        ppm,
        [Description("application/vnd.ms-powerpoint")]
        ppt,
        [Description("application/postscript")]
        ps,
        [Description("video/quicktime")]
        qt,
        [Description("image/x-quicktime")]
        qti,
        [Description("image/x-quicktime")]
        qtif,
        [Description("audio/x-pn-realaudio")]
        ra,
        [Description("audio/x-pn-realaudio")]
        ram,
        [Description("image/x-cmu-raster")]
        ras,
        [Description("application/rdf+xml")]
        rdf,
        [Description("image/x-rgb")]
        rgb,
        [Description("application/vnd.rn-realmedia")]
        rm,
        [Description("application/x-troff")]
        roff,
        [Description("text/rtf")]
        rtf,
        [Description("text/richtext")]
        rtx,
        [Description("text/sgml")]
        sgm,
        [Description("text/sgml")]
        sgml,
        [Description("application/x-sh")]
        sh,
        [Description("application/x-shar")]
        shar,
        [Description("model/mesh")]
        silo,
        [Description("application/x-stuffit")]
        sit,
        [Description("application/x-koan")]
        skd,
        [Description("application/x-koan")]
        skm,
        [Description("application/x-koan")]
        skp,
        [Description("application/x-koan")]
        skt,
        [Description("application/smil")]
        smi,
        [Description("application/smil")]
        smil,
        [Description("audio/basic")]
        snd,
        [Description("application/octet-stream")]
        so,
        [Description("application/x-futuresplash")]
        spl,
        [Description("application/x-wais-source")]
        src,
        [Description("application/x-sv4cpio")]
        sv4cpio,
        [Description("application/x-sv4crc")]
        sv4crc,
        [Description("image/svg+xml")]
        svg,
        [Description("application/x-shockwave-flash")]
        swf,
        [Description("application/x-troff")]
        t,
        [Description("application/x-tar")]
        tar,
        [Description("application/x-tcl")]
        tcl,
        [Description("application/x-tex")]
        tex,
        [Description("application/x-texinfo")]
        texi,
        [Description("application/x-texinfo")]
        texinfo,
        [Description("image/tiff")]
        tif,
        [Description("image/tiff")]
        tiff,
        [Description("application/x-troff")]
        tr,
        [Description("text/tab-separated-values")]
        tsv,
        [Description("text/plain")]
        txt,
        [Description("application/x-ustar")]
        ustar,
        [Description("application/x-cdlink")]
        vcd,
        [Description("model/vrml")]
        vrml,
        [Description("application/voicexml+xml")]
        vxml,
        [Description("audio/x-wav")]
        wav,
        [Description("image/vnd.wap.wbmp")]
        wbmp,
        [Description("application/vnd.wap.wbxml")]
        wbmxl,
        [Description("text/vnd.wap.wml")]
        wml,
        [Description("application/vnd.wap.wmlc")]
        wmlc,
        [Description("text/vnd.wap.wmlscript")]
        wmls,
        [Description("application/vnd.wap.wmlscriptc")]
        wmlsc,
        [Description("model/vrml")]
        wrl,
        [Description("image/x-xbitmap")]
        xbm,
        [Description("application/xhtml+xml")]
        xht,
        [Description("application/xhtml+xml")]
        xhtml,
        [Description("application/vnd.ms-excel")]
        xls,
        [Description("application/xml")]
        xml,
        [Description("image/x-xpixmap")]
        xpm,
        [Description("application/xml")]
        xsl,
        [Description("application/xslt+xml")]
        xslt,
        [Description("application/vnd.mozilla.xul+xml")]
        xul,
        [Description("image/x-xwindowdump")]
        xwd,
        [Description("chemical/x-xyz")]
        xyz,
        [Description("application/zip")]
        zip
    }
}
