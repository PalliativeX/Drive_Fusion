mergeInto(LibraryManager.library, {

    GetLanguageExternal: function () {
        var language = ysdk.environment.i18n.lang;

        //console.log("Language: " + language)

        var bufferSize = lengthBytesUTF8(language) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(language, buffer, bufferSize);
        return buffer;
    },

});