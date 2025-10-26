#!/bin/bash

# Script para zipar projeto no VS Code Terminal (Bash)
# Limite: 9000 KB (9 MB)

# === CONFIGURAÇÕES ===
MAX_SIZE_KB=9000
MAX_SIZE_BYTES=$((MAX_SIZE_KB * 1024))
OUTPUT_FILE="projeto.zip"

# Pastas e arquivos a ignorar
IGNORE_PATTERNS=(
    "node_modules"
    ".git"
    ".vs"
    "bin"
    "obj"
    ".vscode"
    "packages"
    "*.zip"
    "*.log"
    ".DS_Store"
)

# Cores
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
GRAY='\033[0;90m'
NC='\033[0m' # No Color

echo -e "\n${CYAN}📦 COMPACTADOR DE PROJETO - Limite: ${MAX_SIZE_KB} KB${NC}"
echo -e "${GRAY}==================================================${NC}"

# Remove ZIP anterior
if [ -f "$OUTPUT_FILE" ]; then
    rm -f "$OUTPUT_FILE"
    echo -e "${YELLOW}✓ ZIP anterior removido${NC}"
fi

# Cria lista de exclusões para o comando zip
EXCLUDE_ARGS=""
for pattern in "${IGNORE_PATTERNS[@]}"; do
    EXCLUDE_ARGS="$EXCLUDE_ARGS -x '*/$pattern/*' -x '*$pattern*'"
done

echo -e "\n${CYAN}📁 Arquivos a ignorar:${NC}"
for pattern in "${IGNORE_PATTERNS[@]}"; do
    echo -e "   ${GRAY}- $pattern${NC}"
done

echo -e "\n${CYAN}🔄 Compactando arquivos...${NC}"

# Cria o ZIP com exclusões
eval "zip -r -q -9 '$OUTPUT_FILE' . $EXCLUDE_ARGS"

if [ $? -eq 0 ]; then
    # Calcula tamanho do ZIP
    ZIP_SIZE=$(stat -f%z "$OUTPUT_FILE" 2>/dev/null || stat -c%s "$OUTPUT_FILE" 2>/dev/null)
    ZIP_SIZE_KB=$((ZIP_SIZE / 1024))
    PERCENT_USED=$(awk "BEGIN {printf \"%.1f\", ($ZIP_SIZE / $MAX_SIZE_BYTES) * 100}")
    
    echo -e "\n${GREEN}✅ ARQUIVO CRIADO COM SUCESSO!${NC}"
    echo -e "${GRAY}==================================================${NC}"
    echo -e "📦 Nome: ${OUTPUT_FILE}"
    echo -e "📊 Tamanho: ${ZIP_SIZE_KB} KB"
    echo -e "📈 Uso: ${PERCENT_USED}% do limite"
    
    if [ $ZIP_SIZE -gt $MAX_SIZE_BYTES ]; then
        echo -e "\n${RED}❌ ERRO: Arquivo excede o limite de ${MAX_SIZE_KB} KB!${NC}"
        echo -e "\n${YELLOW}💡 Sugestões:${NC}"
        echo -e "   ${GRAY}- Remova arquivos grandes desnecessários${NC}"
        echo -e "   ${GRAY}- Adicione mais pastas à lista de ignorados${NC}"
        echo -e "   ${GRAY}- Verifique arquivos de build (bin/obj)${NC}"
    else
        echo -e "\n${GREEN}✨ Arquivo dentro do limite!${NC}"
    fi
else
    echo -e "\n${RED}❌ ERRO ao criar ZIP${NC}"
    exit 1
fi

echo ""