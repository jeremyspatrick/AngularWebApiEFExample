String.prototype.toCamelCase = function () {
    return this
        .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
        .replace(/\s/g, '')
        .replace(/^(.)/, function ($1) { return $1.toLowerCase(); });
}

function enumString(e,value) 
{
    for (var k in e) if (e[k] == value) return k;
    return null;
}