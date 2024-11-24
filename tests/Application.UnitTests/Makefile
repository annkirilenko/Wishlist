COVERAGE_DIR = ./coverage
HTML_COVERAGE_DIR = ./coverage/html
REPORT_FILE = $(COVERAGE_DIR)/$(shell ls $(COVERAGE_DIR) | head -n 1)/coverage.cobertura.xml

clear-folder: 
	@rm -rf $(COVERAGE_DIR)/*
	
run-test:
	@dotnet test --collect:"XPlat Code Coverage" --results-directory $(COVERAGE_DIR)
	
gen-report:
	@reportgenerator -reports:$(REPORT_FILE) -targetdir:$(HTML_COVERAGE_DIR)

coverage: clear-folder run-test gen-report
    
 